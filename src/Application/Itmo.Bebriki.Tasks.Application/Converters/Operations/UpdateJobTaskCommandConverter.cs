using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Converters.Operations;

internal static class UpdateJobTaskCommandConverter
{
    internal static UpdateJobTaskContext ToContext(UpdateJobTaskCommand command, DateTimeOffset updatedAt)
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