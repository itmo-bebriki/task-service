using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public static class JobTaskFactory
{
    public static JobTask CreateNew(CreateJobTaskContext context)
    {
        return new JobTask
        {
            Title = context.Title,
            Description = context.Description,
            AssigneeId = context.AssigneeId,
            State = JobTaskState.Backlog,
            Priority = context.Priority,
            DependOnTasks = context.DependOnTasks,
            DeadLine = context.DeadLine,
        };
    }

    public static JobTask CreateFromUpdate(JobTask jobTask, UpdateJobTaskContext context)
    {
        // TODO валидация состояний
        var updatedDependOnTasks = new HashSet<long>(jobTask.DependOnTasks);

        if (context.DependOnTaskId.HasValue)
        {
            updatedDependOnTasks.Add(context.DependOnTaskId.Value);
        }

        return new JobTask
        {
            Title = context.Title ?? jobTask.Title,
            Description = context.Description,
            AssigneeId = context.AssigneeId ?? jobTask.AssigneeId,
            State = context.State ?? jobTask.State,
            Priority = context.Priority ?? jobTask.Priority,
            DependOnTasks = updatedDependOnTasks,
            DeadLine = context.DeadLine ?? jobTask.DeadLine,
            IsAgreed = context.IsAgreed ?? jobTask.IsAgreed,
            UpdatedAt = context.UpdatedAt,
        };
    }
}