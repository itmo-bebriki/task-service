using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class UpdateJobTaskCommandConverter
{
    public static UpdateJobTaskContext ToContext(UpdateJobTaskCommand command, DateTimeOffset updatedAt)
    {
        return new UpdateJobTaskContext(
            UpdatedAt: updatedAt,
            Title: command.Title,
            Description: command.Description,
            AssigneeId: command.AssigneeId,
            State: command.State,
            Priority: command.Priority,
            DeadLine: command.DeadLine);
    }
}