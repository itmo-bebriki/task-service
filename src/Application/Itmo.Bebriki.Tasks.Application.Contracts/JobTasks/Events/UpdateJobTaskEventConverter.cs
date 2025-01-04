using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public static class UpdateJobTaskEventConverter
{
    public static UpdateJobTaskEvent ToEvent(JobTask jobTask)
    {
        // TODO
        return new UpdateJobTaskEvent();
    }
}