using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

internal static class JobTaskStateConverter
{
    internal static JobTaskState ToInternal(Contracts.JobTaskState state)
    {
        return state switch
        {
            Contracts.JobTaskState.Unspecified => JobTaskState.None,
            Contracts.JobTaskState.Backlog => JobTaskState.Backlog,
            Contracts.JobTaskState.ToDo => JobTaskState.ToDo,
            Contracts.JobTaskState.InProgress => JobTaskState.InProgress,
            Contracts.JobTaskState.InReview => JobTaskState.InReview,
            Contracts.JobTaskState.Done => JobTaskState.Done,
            Contracts.JobTaskState.Closed => JobTaskState.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }

    internal static Contracts.JobTaskState FromInternal(JobTaskState state)
    {
        return state switch
        {
            JobTaskState.None => Contracts.JobTaskState.Unspecified,
            JobTaskState.Backlog => Contracts.JobTaskState.Backlog,
            JobTaskState.ToDo => Contracts.JobTaskState.ToDo,
            JobTaskState.InProgress => Contracts.JobTaskState.InProgress,
            JobTaskState.InReview => Contracts.JobTaskState.InReview,
            JobTaskState.Done => Contracts.JobTaskState.Done,
            JobTaskState.Closed => Contracts.JobTaskState.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }
}