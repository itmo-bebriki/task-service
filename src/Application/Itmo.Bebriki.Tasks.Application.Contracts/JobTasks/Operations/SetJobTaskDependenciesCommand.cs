namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public sealed record SetJobTaskDependenciesCommand(long JobTaskId, IReadOnlySet<long> DependOnJobTaskIds);