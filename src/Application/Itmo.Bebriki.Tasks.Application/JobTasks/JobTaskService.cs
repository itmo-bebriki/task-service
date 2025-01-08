using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Contracts.Exceptions;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
using Itmo.Bebriki.Tasks.Application.Converters.Dtos;
using Itmo.Bebriki.Tasks.Application.Converters.Events;
using Itmo.Bebriki.Tasks.Application.Converters.Operations;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Itmo.Bebriki.Tasks.Application.JobTasks;

internal sealed class JobTaskService : IJobTaskService
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<JobTaskService> _logger;

    public JobTaskService(
        IPersistenceContext persistenceContext,
        IPersistenceTransactionProvider transactionProvider,
        IDateTimeProvider dateTimeProvider,
        IEventPublisher eventPublisher,
        ILogger<JobTaskService> logger)
    {
        _persistenceContext = persistenceContext;
        _transactionProvider = transactionProvider;
        _dateTimeProvider = dateTimeProvider;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<JobTaskDto> GetJobTaskByIdAsync(
        GetJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        var jobTaskQuery = JobTaskQuery.Build(builder => builder
            .WithJobTaskId(command.JobTaskId)
            .WithPageSize(1));

        JobTask? jobTask = await _persistenceContext.JobTasks
            .QueryAsync(jobTaskQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (jobTask is null)
        {
            _logger.LogWarning($"Job task with id {command.JobTaskId} not found.");
            throw new JobTaskNotFoundException($"Job task with id {command.JobTaskId} not found.");
        }

        JobTaskDto jobTaskDto = JobTaskDtoConverter.ToDto(jobTask);

        return jobTaskDto;
    }

    public async Task<PagedJobTaskDtos> QueryJobTasksAsync(
        QueryJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        JobTaskQuery jobTaskQuery = QueryJobTaskCommandConverter.ToQuery(command);

        HashSet<JobTaskDto> jobTaskDtos = await _persistenceContext.JobTasks
            .QueryAsync(jobTaskQuery, cancellationToken)
            .Select(JobTaskDtoConverter.ToDto)
            .ToHashSetAsync(cancellationToken);

        long? cursor = jobTaskDtos.Count == command.PageSize && jobTaskDtos.Count > 0
            ? jobTaskDtos.Last().Id
            : null;

        return new PagedJobTaskDtos(cursor, jobTaskDtos);
    }

    public async Task<long> CreateJobTaskAsync(
        CreateJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        await CheckForExistingJobTasksAsync(command.DependOnTasks, cancellationToken);

        CreateJobTaskContext context = CreateJobTaskCommandConverter.ToContext(command, _dateTimeProvider.Current);
        JobTask jobTask = JobTaskFactory.CreateFromCreateContext(context);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            long jobTaskId = await _persistenceContext.JobTasks
                .AddAsync([jobTask], cancellationToken)
                .FirstAsync(cancellationToken);

            CreateJobTaskEvent createJobTaskEvent = CreateJobTaskEventConverter.ToEvent(jobTaskId, jobTask);

            await _eventPublisher.PublishAsync(createJobTaskEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return jobTaskId;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create job task. JobTaskId: {JobTaskId}, Title: {Title}, AssigneeId: {AssigneeId}, State: {State}, Priority: {Priority}, DeadLine: {DeadLine}, IsAgreed: {IsAgreed}, DependOnTaskIds: {DependOnTaskIds}",
                jobTask.Id,
                jobTask.Title,
                jobTask.AssigneeId,
                jobTask.State,
                jobTask.Priority,
                jobTask.DeadLine,
                jobTask.IsAgreed,
                string.Join(", ", jobTask.DependOnJobTaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateJobTaskAsync(
        UpdateJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        var jobTaskQuery = JobTaskQuery.Build(builder => builder
            .WithJobTaskId(command.JobTaskId)
            .WithPageSize(1));

        JobTask? jobTask = await _persistenceContext.JobTasks
            .QueryAsync(jobTaskQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (jobTask is null)
        {
            _logger.LogWarning($"Job task with id {command.JobTaskId} not found.");
            throw new JobTaskNotFoundException($"Job task with id {command.JobTaskId} not found.");
        }

        UpdateJobTaskContext context = UpdateJobTaskCommandConverter.ToContext(command, _dateTimeProvider.Current);
        JobTask updatedJobTask = JobTaskFactory.CreateFromUpdateContext(jobTask, context);

        UpdateJobTaskEvent updateJobTaskEvent = UpdateJobTaskEventConverter.ToEvent(jobTask, updatedJobTask);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.JobTasks.UpdateAsync([updatedJobTask], cancellationToken);

            await _eventPublisher.PublishAsync(updateJobTaskEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update job task. JobTaskId: {JobTaskId}, Updated Fields: {UpdatedFields}",
                updatedJobTask.Id,
                GetUpdatedFields(context));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken)
    {
        await CheckForExistingJobTasksAsync(
            new HashSet<long>(command.DependOnJobTaskIds) { command.JobTaskId },
            cancellationToken);
        await CheckForCyclicDependencyAsync(command.JobTaskId, command.DependOnJobTaskIds, cancellationToken);

        var jobTaskDependenciesQuery = JobTaskDependenciesQuery.Build(builder => builder
            .WithJobTaskId(command.JobTaskId)
            .WithDependOnIds(command.DependOnJobTaskIds));

        AddJobTaskDependenciesEvent jobTaskDependenciesEvent =
            AddJobTaskDependenciesEventConverter.ToEvent(command.JobTaskId, command.DependOnJobTaskIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.JobTasks.AddDependenciesAsync(jobTaskDependenciesQuery, cancellationToken);

            await _eventPublisher.PublishAsync(jobTaskDependenciesEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to add dependencies to job task. JobTaskId: {JobTaskId}, Dependencies: {Dependencies}",
                command.JobTaskId,
                string.Join(", ", command.DependOnJobTaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken)
    {
        await CheckForExistingJobTasksAsync(
            new HashSet<long>(command.DependOnJobTaskIds) { command.JobTaskId },
            cancellationToken);
        await CheckForCyclicDependencyAsync(command.JobTaskId, command.DependOnJobTaskIds, cancellationToken);

        var jobTaskDependenciesQuery = JobTaskDependenciesQuery.Build(builder => builder
            .WithJobTaskId(command.JobTaskId)
            .WithDependOnIds(command.DependOnJobTaskIds));

        RemoveJobTaskDependenciesEvent jobTaskDependenciesEvent =
            RemoveJobTaskDependenciesEventConverter.ToEvent(command.JobTaskId, command.DependOnJobTaskIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.JobTasks.RemoveDependenciesAsync(jobTaskDependenciesQuery, cancellationToken);

            await _eventPublisher.PublishAsync(jobTaskDependenciesEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to remove dependencies from job task. JobTaskId: {JobTaskId}, Dependencies: {Dependencies}",
                command.JobTaskId,
                string.Join(", ", command.DependOnJobTaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static string GetUpdatedFields(UpdateJobTaskContext context)
    {
        var updatedFields = new List<string>();

        if (context.Title != null) updatedFields.Add(nameof(context.Title));
        if (context.Description != null) updatedFields.Add(nameof(context.Description));
        if (context.AssigneeId != null) updatedFields.Add(nameof(context.AssigneeId));
        if (context.State != null) updatedFields.Add(nameof(context.State));
        if (context.Priority != null) updatedFields.Add(nameof(context.Priority));
        if (context.DeadLine != null) updatedFields.Add(nameof(context.DeadLine));
        if (context.IsAgreed != null) updatedFields.Add(nameof(context.IsAgreed));

        return updatedFields.Any() ? string.Join(", ", updatedFields) : "None";
    }

    private async Task CheckForExistingJobTasksAsync(
        IReadOnlySet<long> jobTaskIds,
        CancellationToken cancellationToken)
    {
        var jobTaskQuery = JobTaskQuery.Build(builder => builder
            .WithJobTaskIds(jobTaskIds)
            .WithPageSize(jobTaskIds.Count));

        List<long> existingJobTaskIds = await _persistenceContext.JobTasks
            .QueryAsync(jobTaskQuery, cancellationToken)
            .Select(jobTask => jobTask.Id)
            .ToListAsync(cancellationToken);

        var missingIds = jobTaskIds
            .Except(existingJobTaskIds)
            .ToList();

        if (missingIds.Count != 0)
        {
            string missingIdsString = string.Join(", ", missingIds);

            _logger.LogWarning($"Job tasks with ids {missingIdsString} not found.");
            throw new JobTaskNotFoundException($"Job tasks with ids {missingIdsString} not found.");
        }
    }

    private async Task CheckForCyclicDependencyAsync(
        long jobTaskId,
        IReadOnlySet<long> dependOnJobTaskIds,
        CancellationToken cancellationToken)
    {
        if (dependOnJobTaskIds.Contains(jobTaskId))
        {
            _logger.LogWarning($"Cyclic relationship between job tasks found: {jobTaskId} and {jobTaskId}");
            throw new JobTaskCyclicDependencyException(
                $"Cyclic relationship between job tasks found: {jobTaskId} and {jobTaskId}");
        }

        var dependentJobTaskQuery = DependentJobTaskQuery.Build(builder => builder.WithJobTaskId(jobTaskId));

        List<JobTask> dependentJobTasks = await _persistenceContext.JobTasks
            .GetDependentJobTasksAsync(dependentJobTaskQuery, cancellationToken)
            .ToListAsync(cancellationToken);

        foreach (JobTask jobTask in dependentJobTasks)
        {
            if (dependOnJobTaskIds.Contains(jobTask.Id))
            {
                _logger.LogWarning($"Cyclic relationship between job tasks found: {jobTaskId} and {jobTask.Id}");
                throw new JobTaskCyclicDependencyException(
                    $"Cyclic relationship between job tasks found: {jobTaskId} and {jobTask.Id}");
            }
        }
    }
}