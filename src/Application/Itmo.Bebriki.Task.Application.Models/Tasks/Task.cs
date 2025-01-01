namespace Itmo.Bebriki.Task.Application.Models.Tasks;

public sealed record Task(
    long Id,
    string Title,
    string? Description,
    long AssigneeId,
    TaskState State,
    TaskPriority Priority,
    IReadOnlyList<long> DependOnTasks,
    DateTimeOffset? DeadLine,
    DateTimeOffset UpdatedAt);