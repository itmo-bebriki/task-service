using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
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

    public async Task<long> CreateJobTaskAsync(
        CreateJobTaskCommand command,
        CancellationToken cancellationToken)
    {
        CreateJobTaskContext context = CreateJobTaskCommandConverter.ToContext(command);
        JobTask jobTask = JobTaskFactory.CreateNew(context);

        CreateJobTaskEvent createJobTaskEvent = CreateJobTaskEventConverter.ToEvent(jobTask);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        long newId = await _persistenceContext.JobTasks
            .AddAsync([jobTask], cancellationToken)
            .FirstAsync(cancellationToken);
        await _eventPublisher.PublishAsync(createJobTaskEvent, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return newId;
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
        JobTask updatedJobTask = JobTaskFactory.CreateFromUpdate(jobTask, context);

        await _persistenceContext.JobTasks.UpdateAsync([updatedJobTask], cancellationToken);
    }
}