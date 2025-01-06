using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public sealed record UpdateJobTaskEvent(
    long JobTaskId,
    string? Title = null,
    string? Description = null,
    long? AssigneeId = null,
    JobTaskState? State = null,
    JobTaskPriority? Priority = null,
    DateTimeOffset? DeadLine = null,
    bool? IsAgreed = null) : IEvent;