using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskSubmissionConverter
{
    internal static JobTaskSubmissionValue ToValue(CreateJobTaskEvent evt)
    {
        return new JobTaskSubmissionValue
        {
            JobTaskCreateSubmission = new JobTaskSubmissionValue.Types.JobTaskCreateSubmission
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

    internal static JobTaskSubmissionValue ToValue(UpdateJobTaskEvent evt)
    {
        return new JobTaskSubmissionValue
        {
            JobTaskUpdateSubmission = new JobTaskSubmissionValue.Types.JobTaskUpdateSubmission
            {
                JobTaskId = evt.JobTaskId,
                NewAssigneeId = evt.AssigneeId,
                NewDeadline = evt.DeadLine?.ToTimestamp(),
            },
        };
    }
}