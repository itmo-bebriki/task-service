using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Converters.Events;

internal static class UpdateJobTaskEventConverter
{
    internal static UpdateJobTaskEvent ToEvent(JobTask jobTask, JobTask updatedJobTask)
    {
        return new UpdateJobTaskEvent(
            JobTaskId: updatedJobTask.Id,
            UpdateAt: updatedJobTask.UpdatedAt,
            Title: jobTask.Title == updatedJobTask.Title ? null : updatedJobTask.Title,
            Description: jobTask.Description == updatedJobTask.Description ? null : updatedJobTask.Description,
            AssigneeId: jobTask.AssigneeId == updatedJobTask.AssigneeId ? null : updatedJobTask.AssigneeId,
            State: jobTask.State == updatedJobTask.State ? null : updatedJobTask.State,
            Priority: jobTask.Priority == updatedJobTask.Priority ? null : updatedJobTask.Priority,
            DeadLine: jobTask.DeadLine == updatedJobTask.DeadLine ? null : updatedJobTask.DeadLine);
    }
}