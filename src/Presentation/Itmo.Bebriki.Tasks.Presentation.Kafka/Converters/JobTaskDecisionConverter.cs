using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskDecisionConverter
{
    internal static UpdateJobTaskCommand ToInternal(JobTaskDecisionKey key, JobTaskDecisionValue value)
    {
        return new UpdateJobTaskCommand(
            JobTaskId: key.JobTaskId,
            AssigneeId: value.ApprovedAssigneeId,
            State: JobTaskStateConverter.ToInternal(value.State),
            DeadLine: value.ApprovedDeadline?.ToDateTimeOffset());
    }
}