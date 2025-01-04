namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

public sealed record UpdateJobTaskContext(
    DateTimeOffset UpdatedAt,
    string? Title = null,
    string? Description = null,
    long? AssigneeId = null,
    JobTaskState? State = null,
    JobTaskPriority? Priority = null,
    DateTimeOffset? DeadLine = null,
    bool? IsAgreed = null);