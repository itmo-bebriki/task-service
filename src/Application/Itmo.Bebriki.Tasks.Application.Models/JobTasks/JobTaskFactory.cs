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
            State = JobTaskState.PendingApproval,
            Priority = context.Priority,
            DependOnJobTaskIds = context.DependOnTasks,
            DeadLine = context.DeadLine,
            UpdatedAt = context.CreatedAt,
        };
    }

    public static JobTask CreateFromUpdateContext(UpdateJobTaskContext context, JobTask prevJobTask)
    {
        long? assigneeId = prevJobTask.AssigneeId;
        DateTimeOffset? deadline = prevJobTask.DeadLine;
        JobTaskState state = prevJobTask.State;

        if (context.State is null)
        {
            deadline = prevJobTask.DeadLine;
            assigneeId = prevJobTask.AssigneeId;

            if (context.DeadLine is not null || context.AssigneeId is not null)
            {
                state = JobTaskState.PendingApproval;
            }
        }
        else if (context.State is JobTaskState.Approved)
        {
            deadline = context.DeadLine;
            assigneeId = context.AssigneeId;
            state = JobTaskState.Approved;
        }
        else if (context.State is JobTaskState.Rejected)
        {
            deadline = null;
            assigneeId = null;
            state = JobTaskState.Rejected;
        }

        return new JobTask
        {
            Id = context.JobTaskId,
            Title = context.Title ?? prevJobTask.Title,
            Description = context.Description ?? prevJobTask.Description,
            AssigneeId = assigneeId ?? prevJobTask.AssigneeId,
            State = state,
            Priority = context.Priority ?? prevJobTask.Priority,
            DependOnJobTaskIds = prevJobTask.DependOnJobTaskIds,
            DeadLine = deadline ?? prevJobTask.DeadLine,
            UpdatedAt = context.UpdatedAt,
        };
    }
}