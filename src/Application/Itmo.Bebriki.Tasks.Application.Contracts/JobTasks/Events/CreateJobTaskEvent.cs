using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public sealed record CreateJobTaskEvent(
    long JobTaskId,
    string Title,
    string Description,
    long AssigneeId,
    JobTaskPriority Priority,
    IReadOnlySet<long> DependOnTasks,
    DateTimeOffset DeadLine,
    DateTimeOffset CreatedAt) : IEvent;