namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public sealed record JobTask(
    long Id,
    string Title,
    string? Description,
    long AssigneeId,
    JobTaskState State,
    JobTaskPriority Priority,
    IReadOnlyList<long> DependOnTasks,
    DateTimeOffset? DeadLine,
    DateTimeOffset UpdatedAt);