using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class UpdateJobTaskEventConverter
{
    internal static UpdateJobTaskEvent ToEvent(JobTask jobTask)
    {
        // TODO
        return new UpdateJobTaskEvent();
    }
}