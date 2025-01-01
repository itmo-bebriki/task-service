using Itmo.Bebriki.Task.Application.Models.Tasks;

namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Dtos;

public sealed record TaskDto
{
    public long Id { get; }
    public string Title { get; }

    public string? Description { get; }

    public long AssigneeId { get; }

    public TaskState State { get; }

    public TaskPriority Priority { get; }

    public IReadOnlyList<long> DependOnTasks { get; }

    public DateTimeOffset? DeadLine { get; }

    public DateTimeOffset UpdatedAt { get; }

    public TaskDto(
        long id,
        string title,
        string? description,
        long assigneeId,
        TaskState state,
        TaskPriority priority,
        IReadOnlyList<long> dependOnTasks,
        DateTimeOffset? deadLine,
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
        UpdatedAt = updateAt;
    }
}