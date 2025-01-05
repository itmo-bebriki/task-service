using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

public sealed record JobTaskDto
{
    public long Id { get; }

    public string Title { get; }

    public string? Description { get; }

    public long AssigneeId { get; }

    public JobTaskState State { get; }

    public JobTaskPriority Priority { get; }

    public IReadOnlySet<long> DependOnTasks { get; }

    public DateTimeOffset? DeadLine { get; }

    public bool IsAgreed { get; }

    public DateTimeOffset UpdatedAt { get; }

    public JobTaskDto(
        long id,
        string title,
        string? description,
        long assigneeId,
        JobTaskState state,
        JobTaskPriority priority,
        IReadOnlySet<long> dependOnTasks,
        DateTimeOffset? deadLine,
        bool isAgreed,
        DateTimeOffset updateAt)
    {
        // TODO валидация
        Id = id;
        Title = title;
        Description = description;
        AssigneeId = assigneeId;
        State = state;
        Priority = priority;
        DependOnTasks = dependOnTasks;
        DeadLine = deadLine;
        IsAgreed = isAgreed;
        UpdatedAt = updateAt;
    }
}