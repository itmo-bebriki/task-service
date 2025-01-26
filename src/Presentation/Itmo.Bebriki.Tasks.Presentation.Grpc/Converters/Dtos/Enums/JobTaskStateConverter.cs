using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

internal static class JobTaskStateConverter
{
    internal static JobTaskState ToInternal(Contracts.JobTaskState state)
    {
        return state switch
        {
            Contracts.JobTaskState.Unspecified => JobTaskState.None,
            Contracts.JobTaskState.PendingApproval => JobTaskState.PendingApproval,
            Contracts.JobTaskState.Approved => JobTaskState.Approved,
            Contracts.JobTaskState.Rejected => JobTaskState.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }

    internal static Contracts.JobTaskState FromInternal(JobTaskState state)
    {
        return state switch
        {
            JobTaskState.None => Contracts.JobTaskState.Unspecified,
            JobTaskState.PendingApproval => Contracts.JobTaskState.PendingApproval,
            JobTaskState.Approved => Contracts.JobTaskState.Approved,
            JobTaskState.Rejected => Contracts.JobTaskState.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }
}