using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class CreateJobTaskCommandConverter
{
    public static CreateJobTaskContext ToContext(CreateJobTaskCommand command, DateTimeOffset createdAt)
    {
        var dependOnTasksSet = new HashSet<long>(command.DependOnTasks);

        return new CreateJobTaskContext(
            Title: command.Title,
            Description: command.Description,
            AssigneeId: command.AssigneeId,
            Priority: command.Priority,
            DependOnTasks: dependOnTasksSet,
            DeadLine: command.DeadLine,
            CreatedAt: createdAt);
    }
}