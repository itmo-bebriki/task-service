namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public static class AddJobTaskDependenciesEventConverter
{
    public static AddJobTaskDependenciesEvent ToEvent(long jobTaskId, IReadOnlyCollection<long> dependencies)
    {
        // TODO
        return new AddJobTaskDependenciesEvent();
    }
}