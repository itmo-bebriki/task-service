using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Converters.Commands;

internal static class UpdateJobTaskCommandConverter
{
    internal static UpdateJobTaskContext ToContext(
        UpdateJobTaskCommand command,
        JobTask prevJobTask,
        DateTimeOffset updatedAt)
    {
        return new UpdateJobTaskContext(
            JobTaskId: prevJobTask.Id,
            Title: command.Title ?? prevJobTask.Title,
            Description: command.Description ?? prevJobTask.Description,
            AssigneeId: command.AssigneeId ?? prevJobTask.AssigneeId,
            State: command.State ?? prevJobTask.State,
            Priority: command.Priority ?? prevJobTask.Priority,
            DependsOnJobTaskIds: prevJobTask.DependOnJobTaskIds,
            DeadLine: command.DeadLine ?? prevJobTask.DeadLine,
            IsAgreed: command.IsAgreed ?? prevJobTask.IsAgreed,
            UpdatedAt: updatedAt);
    }
}