using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskSubmissionConverter
{
    internal static JobTaskSubmissionValue ToValue(CreateJobTaskEvent evt)
    {
        return new JobTaskSubmissionValue
        {
            JobTaskId = evt.JobTaskId,
            NewAssigneeId = null,
            NewDeadline = null,
        };
    }

    internal static JobTaskSubmissionValue ToValue(UpdateJobTaskEvent evt)
    {
        return new JobTaskSubmissionValue
        {
            JobTaskId = evt.JobTaskId,
            NewAssigneeId = evt.AssigneeId,
            NewDeadline = evt.DeadLine?.ToTimestamp(),
        };
    }
}