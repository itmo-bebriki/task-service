using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class RemoveJobTaskDependenciesEventConverter
{
    internal static RemoveJobTaskDependenciesEvent ToEvent(
        long jobTaskId,
        IReadOnlySet<long> dependOnJobTaskIds,
        DateTimeOffset updatedAt)
    {
        return new RemoveJobTaskDependenciesEvent(jobTaskId, dependOnJobTaskIds, updatedAt);
    }
}