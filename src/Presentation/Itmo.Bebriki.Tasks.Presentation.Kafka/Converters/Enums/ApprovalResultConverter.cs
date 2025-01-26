using Itmo.Bebriki.Tasks.Kafka.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

internal static class ApprovalResultConverter
{
    internal static Application.Models.JobTasks.JobTaskState ToInternal(ApprovalResult result)
    {
        return result switch
        {
            ApprovalResult.Unspecified => Application.Models.JobTasks.JobTaskState.None,
            ApprovalResult.Approved => Application.Models.JobTasks.JobTaskState.Approved,
            ApprovalResult.Rejected => Application.Models.JobTasks.JobTaskState.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null),
        };
    }
}