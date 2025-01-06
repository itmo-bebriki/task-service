using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class CreateJobTaskEventConverter
{
    internal static CreateJobTaskEvent ToEvent(long jobTaskId, JobTask jobTask)
    {
        return new CreateJobTaskEvent(
            JobTaskId: jobTaskId,
            Title: jobTask.Title,
            Description: jobTask.Description,
            AssigneeId: jobTask.AssigneeId,
            Priority: jobTask.Priority,
            DependOnTasks: jobTask.DependOnJobTaskIds,
            DeadLine: jobTask.DeadLine);
    }
}