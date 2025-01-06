namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

public sealed record CreateJobTaskContext(
    string Title,
    string Description,
    long AssigneeId,
    JobTaskPriority Priority,
    IReadOnlySet<long> DependOnTasks,
    DateTimeOffset DeadLine,
    DateTimeOffset CreatedAt);