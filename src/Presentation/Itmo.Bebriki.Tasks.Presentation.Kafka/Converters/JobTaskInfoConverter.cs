using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskInfoConverter
{
    internal static JobTaskInfoValue ToValue(CreateJobTaskEvent evt)
    {
        return new JobTaskInfoValue
        {
            JobTaskCreated = new JobTaskInfoValue.Types.JobTaskCreated
            {
                JobTaskId = evt.JobTaskId,
                Title = evt.Title,
                Description = evt.Description,
                AssigneeId = evt.AssigneeId,
                Priority = JobTaskPriorityConverter.FromInternal(evt.Priority),
                DeadLine = evt.DeadLine.ToTimestamp(),
                CreatedAt = evt.CreatedAt.ToTimestamp(),
            },
        };
    }

    internal static JobTaskInfoValue ToValue(UpdateJobTaskEvent evt)
    {
        return new JobTaskInfoValue
        {
            JobTaskUpdated = new JobTaskInfoValue.Types.JobTaskUpdated
            {
                JobTaskId = evt.JobTaskId,
                Title = evt.Title,
                Description = evt.Description,
                AssigneeId = evt.AssigneeId,
                State = evt.State is null
                    ? JobTaskState.Unspecified
                    : JobTaskStateConverter.FromInternal(evt.State.Value),
                Priority = evt.Priority is null
                    ? JobTaskPriority.Unspecified
                    : JobTaskPriorityConverter.FromInternal(evt.Priority.Value),
                DeadLine = evt.DeadLine?.ToTimestamp(),
                UpdatedAt = evt.UpdateAt.ToTimestamp(),
            },
        };
    }

    internal static JobTaskInfoValue ToValue(AddJobTaskDependenciesEvent evt)
    {
        return new JobTaskInfoValue
        {
            JobTaskDependenciesAdded = new JobTaskInfoValue.Types.JobTaskDependenciesAdded
            {
                JobTaskId = evt.JobTaskId,
                AddedDependencies = { evt.DependOnJobTaskIds.ToArray() },
                UpdatedAt = evt.UpdatedAt.ToTimestamp(),
            },
        };
    }

    internal static JobTaskInfoValue ToValue(RemoveJobTaskDependenciesEvent evt)
    {
        return new JobTaskInfoValue
        {
            JobTaskDependenciesRemoved = new JobTaskInfoValue.Types.JobTaskDependenciesRemoved
            {
                JobTaskId = evt.JobTaskId,
                RemovedDependencies = { evt.DependOnJobTaskIds.ToArray() },
                UpdatedAt = evt.UpdatedAt.ToTimestamp(),
            },
        };
    }
}