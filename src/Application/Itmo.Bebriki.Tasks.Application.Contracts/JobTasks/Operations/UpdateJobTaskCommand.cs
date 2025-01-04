using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public sealed record UpdateJobTaskCommand(
    long JobTaskId,
    string? Title = null,
    string? Description = null,
    long? AssigneeId = null,
    JobTaskState? State = null,
    JobTaskPriority? Priority = null,
    DateTimeOffset? DeadLine = null,
    bool? IsAgreed = null);