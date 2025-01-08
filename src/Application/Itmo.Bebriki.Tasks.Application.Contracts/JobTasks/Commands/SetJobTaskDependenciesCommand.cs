namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;

public sealed record SetJobTaskDependenciesCommand(long JobTaskId, IReadOnlySet<long> DependOnJobTaskIds);