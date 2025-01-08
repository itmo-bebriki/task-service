using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Converters.Operations;

internal static class CreateJobTaskCommandConverter
{
    internal static CreateJobTaskContext ToContext(CreateJobTaskCommand command, DateTimeOffset createdAt)
    {
        return new CreateJobTaskContext(
            Title: command.Title,
            Description: command.Description,
            AssigneeId: command.AssigneeId,
            Priority: command.Priority,
            DependOnTasks: command.DependOnTasks,
            DeadLine: command.DeadLine,
            CreatedAt: createdAt);
    }
}