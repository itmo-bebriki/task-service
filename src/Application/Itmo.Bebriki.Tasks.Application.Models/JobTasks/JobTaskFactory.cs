using Itmo.Bebriki.Tasks.Application.Models.JobTasks.Contexts;

namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public static class JobTaskFactory
{
    public static JobTask CreateNew(
        long id,
        string title,
        string description,
        long assigneeId,
        JobTaskState state,
        JobTaskPriority priority,
        IReadOnlyCollection<long> dependsOnIds,
        DateTimeOffset deadline,
        bool isAgreed,
        DateTimeOffset updatedAt)
    {
        return new JobTask
        {
            Id = id,
            Title = title,
            Description = description,
            AssigneeId = assigneeId,
            State = state,
            Priority = priority,
            DependOnJobTaskIds = new HashSet<long>(dependsOnIds),
            DeadLine = deadline,
            IsAgreed = isAgreed,
            UpdatedAt = updatedAt,
        };
    }

    public static JobTask CreateFromCreateContext(CreateJobTaskContext context)
    {
        return new JobTask
        {
            Title = context.Title,
            Description = context.Description,
            AssigneeId = context.AssigneeId,
            State = JobTaskState.Backlog,
            Priority = context.Priority,
            DependOnJobTaskIds = context.DependOnTasks,
            DeadLine = context.DeadLine,
            UpdatedAt = context.CreatedAt,
        };
    }

    public static JobTask CreateFromUpdateContext(JobTask jobTask, UpdateJobTaskContext context)
    {
        // TODO валидация состояний
        return new JobTask
        {
            Id = jobTask.Id,
            Title = context.Title ?? jobTask.Title,
            Description = context.Description ?? jobTask.Description,
            AssigneeId = context.AssigneeId ?? jobTask.AssigneeId,
            State = context.State ?? jobTask.State,
            Priority = context.Priority ?? jobTask.Priority,
            DependOnJobTaskIds = jobTask.DependOnJobTaskIds,
            DeadLine = context.DeadLine ?? jobTask.DeadLine,
            IsAgreed = context.IsAgreed ?? jobTask.IsAgreed,
            UpdatedAt = context.UpdatedAt,
        };
    }
}