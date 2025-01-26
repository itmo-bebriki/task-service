using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

internal static class JobTaskStateConverter
{
    internal static JobTaskState ToInternal(Tasks.Kafka.Contracts.JobTaskState state)
    {
        return state switch
        {
            Tasks.Kafka.Contracts.JobTaskState.Unspecified => JobTaskState.None,
            Tasks.Kafka.Contracts.JobTaskState.PendingApproval => JobTaskState.PendingApproval,
            Tasks.Kafka.Contracts.JobTaskState.Approved => JobTaskState.Approved,
            Tasks.Kafka.Contracts.JobTaskState.Rejected => JobTaskState.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }

    internal static Tasks.Kafka.Contracts.JobTaskState FromInternal(JobTaskState state)
    {
        return state switch
        {
            JobTaskState.None => Tasks.Kafka.Contracts.JobTaskState.Unspecified,
            JobTaskState.PendingApproval => Tasks.Kafka.Contracts.JobTaskState.PendingApproval,
            JobTaskState.Approved => Tasks.Kafka.Contracts.JobTaskState.Approved,
            JobTaskState.Rejected => Tasks.Kafka.Contracts.JobTaskState.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }
}