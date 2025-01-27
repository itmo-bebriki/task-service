using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public sealed record SubmissionJobTaskEvent(
    long JobTaskId,
    long? AssigneeId,
    DateTimeOffset? DeadLine) : IEvent;