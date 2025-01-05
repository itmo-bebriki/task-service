using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class AddJobTaskDependenciesEventConverter
{
    internal static AddJobTaskDependenciesEvent ToEvent(long jobTaskId, IReadOnlyCollection<long> dependencies)
    {
        // TODO
        return new AddJobTaskDependenciesEvent();
    }
}