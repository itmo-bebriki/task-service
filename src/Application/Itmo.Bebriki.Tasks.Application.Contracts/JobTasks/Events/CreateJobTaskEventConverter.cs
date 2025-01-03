using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;

public static class CreateJobTaskEventConverter
{
    public static CreateJobTaskEvent ToEvent(JobTask jobTask)
    {
        // TODO
        return new CreateJobTaskEvent();
    }
}