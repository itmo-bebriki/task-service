namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

public sealed record UpdateJobTaskContext(
    long JobTaskId,
    string Title,
    string Description,
    long AssigneeId,
    JobTaskState State,
    JobTaskPriority Priority,
    IReadOnlySet<long> DependsOnJobTaskIds,
    DateTimeOffset DeadLine,
    bool IsAgreed,
    DateTimeOffset UpdatedAt);