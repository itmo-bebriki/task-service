using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
using Itmo.Bebriki.Tasks.Application.Converters;
using Itmo.Bebriki.Tasks.Application.Models.Exceptions;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using System.Data;

namespace Itmo.Bebriki.Tasks.Application.JobTasks;

public sealed class JobTaskService : IJobTaskService
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEventPublisher _eventPublisher;

    public JobTaskService(
        IPersistenceContext persistenceContext,
        IPersistenceTransactionProvider transactionProvider,
        IDateTimeProvider dateTimeProvider,
        IEventPublisher eventPublisher)
    {
        _persistenceContext = persistenceContext;
        _transactionProvider = transactionProvider;
        _dateTimeProvider = dateTimeProvider;
        _eventPublisher = eventPublisher;
    }

    // TODO возможно убрать из-за наличия QueryJobTasksAsync
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

        List<JobTaskDto> jobTaskDtos = await _persistenceContext.JobTasks
            .QueryAsync(jobTaskQuery, cancellationToken)
            .Select(JobTaskDtoConverter.ToDto)
            .ToListAsync(cancellationToken);

        long? cursor = jobTaskDtos.Count == command.PageSize
            ? jobTaskDtos.Last().Id
            : null;

        return new PagedJobTaskDtos(cursor, jobTaskDtos);
    }

    public async Task<long> CreateJobTaskAsync(
        CreateJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        // TODO проверка на циклические зависимости
        CheckJobTaskDependencies();

        CreateJobTaskContext context = CreateJobTaskCommandConverter.ToContext(command, _dateTimeProvider.Current);
        JobTask jobTask = JobTaskFactory.CreateFromCreateContext(context);

        CreateJobTaskEvent createJobTaskEvent = CreateJobTaskEventConverter.ToEvent(jobTask);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            long newId = await _persistenceContext.JobTasks
                .AddAsync([jobTask], cancellationToken)
                .FirstAsync(cancellationToken);

            await _eventPublisher.PublishAsync(createJobTaskEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return newId;
        }
        catch
        {
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
            throw new JobTaskNotFoundException($"Job task with id {command.JobTaskId} not found.");
        }

        UpdateJobTaskContext context = UpdateJobTaskCommandConverter.ToContext(command, _dateTimeProvider.Current);
        JobTask updatedJobTask = JobTaskFactory.CreateFromUpdateContext(jobTask, context);

        UpdateJobTaskEvent updateJobTaskEvent = UpdateJobTaskEventConverter.ToEvent(updatedJobTask);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.JobTasks.UpdateAsync([updatedJobTask], cancellationToken);

            await _eventPublisher.PublishAsync(updateJobTaskEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken)
    {
        CheckJobTaskDependencies();

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
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken)
    {
        CheckJobTaskDependencies();

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
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private void CheckJobTaskDependencies()
    {
        throw new NotImplementedException();
    }
}