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

    public static JobTask CreateFromUpdateContext(UpdateJobTaskContext context)
    {
        return new JobTask
        {
            Id = context.JobTaskId,
            Title = context.Title,
            Description = context.Description,
            AssigneeId = context.AssigneeId,
            State = context.State,
            Priority = context.Priority,
            DependOnJobTaskIds = context.DependsOnJobTaskIds,
            DeadLine = context.DeadLine,
            IsAgreed = context.IsAgreed,
            UpdatedAt = context.UpdatedAt,
        };
    }
}