using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class CreateJobTaskEventConverter
{
    internal static CreateJobTaskEvent ToEvent(JobTask jobTask)
    {
        // TODO
        return new CreateJobTaskEvent();
    }
}