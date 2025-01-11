using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public sealed record RemoveJobTaskDependenciesEvent(long JobTaskId, IReadOnlySet<long> DependOnJobTaskIds) : IEvent;