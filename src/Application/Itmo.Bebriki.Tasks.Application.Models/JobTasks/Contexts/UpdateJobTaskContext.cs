namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

public sealed record UpdateJobTaskContext(
    long JobTaskId,
    DateTimeOffset UpdatedAt,
    string? Title = null,
    string? Description = null,
    long? AssigneeId = null,
    JobTaskState? State = null,
    JobTaskPriority? Priority = null,
    DateTimeOffset? DeadLine = null);