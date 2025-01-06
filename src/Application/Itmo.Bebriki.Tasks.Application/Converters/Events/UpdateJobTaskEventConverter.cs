using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class UpdateJobTaskEventConverter
{
    internal static UpdateJobTaskEvent ToEvent(JobTask jobTask)
    {
        return new UpdateJobTaskEvent(
            JobTaskId: jobTask.Id,
            Title: jobTask.Title,
            Description: jobTask.Description,
            AssigneeId: jobTask.AssigneeId,
            State: jobTask.State,
            Priority: jobTask.Priority,
            DeadLine: jobTask.DeadLine,
            IsAgreed: jobTask.IsAgreed);
    }
}