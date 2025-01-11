using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public sealed record AddJobTaskDependenciesEvent(long JobTaskId, IReadOnlySet<long> DependOnJobTaskIds) : IEvent;