using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class AddJobTaskDependenciesEventConverter
{
    internal static AddJobTaskDependenciesEvent ToEvent(
        long jobTaskId,
        IReadOnlySet<long> dependOnJobTaskIds,
        DateTimeOffset updatedAt)
    {
        return new AddJobTaskDependenciesEvent(jobTaskId, dependOnJobTaskIds, updatedAt);
    }
}