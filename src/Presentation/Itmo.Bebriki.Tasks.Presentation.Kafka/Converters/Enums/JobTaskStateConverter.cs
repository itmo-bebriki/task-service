using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

internal static class JobTaskStateConverter
{
    internal static JobTaskState ToInternal(Tasks.Kafka.Contracts.JobTaskState state)
    {
        return state switch
        {
            Tasks.Kafka.Contracts.JobTaskState.Unspecified => JobTaskState.None,
            Tasks.Kafka.Contracts.JobTaskState.Backlog => JobTaskState.Backlog,
            Tasks.Kafka.Contracts.JobTaskState.ToDo => JobTaskState.ToDo,
            Tasks.Kafka.Contracts.JobTaskState.InProgress => JobTaskState.InProgress,
            Tasks.Kafka.Contracts.JobTaskState.InReview => JobTaskState.InReview,
            Tasks.Kafka.Contracts.JobTaskState.Done => JobTaskState.Done,
            Tasks.Kafka.Contracts.JobTaskState.Closed => JobTaskState.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }

    internal static Tasks.Kafka.Contracts.JobTaskState FromInternal(JobTaskState state)
    {
        return state switch
        {
            JobTaskState.None => Tasks.Kafka.Contracts.JobTaskState.Unspecified,
            JobTaskState.Backlog => Tasks.Kafka.Contracts.JobTaskState.Backlog,
            JobTaskState.ToDo => Tasks.Kafka.Contracts.JobTaskState.ToDo,
            JobTaskState.InProgress => Tasks.Kafka.Contracts.JobTaskState.InProgress,
            JobTaskState.InReview => Tasks.Kafka.Contracts.JobTaskState.InReview,
            JobTaskState.Done => Tasks.Kafka.Contracts.JobTaskState.Done,
            JobTaskState.Closed => Tasks.Kafka.Contracts.JobTaskState.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }
}