namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public static class RemoveJobTaskDependenciesEventConverter
{
    public static RemoveJobTaskDependenciesEvent ToEvent(long jobTaskId, IReadOnlyCollection<long> dependencies)
    {
        // TODO
        return new RemoveJobTaskDependenciesEvent();
    }
}