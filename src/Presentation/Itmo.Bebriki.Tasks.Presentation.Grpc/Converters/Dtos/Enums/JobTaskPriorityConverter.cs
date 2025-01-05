using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

internal static class JobTaskPriorityConverter
{
    internal static JobTaskPriority ToInternal(Contracts.JobTaskPriority priority)
    {
        return priority switch
        {
            Contracts.JobTaskPriority.Unspecified => JobTaskPriority.None,
            Contracts.JobTaskPriority.Low => JobTaskPriority.Low,
            Contracts.JobTaskPriority.Medium => JobTaskPriority.Medium,
            Contracts.JobTaskPriority.High => JobTaskPriority.High,
            Contracts.JobTaskPriority.Critical => JobTaskPriority.Critical,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null),
        };
    }

    internal static Contracts.JobTaskPriority FromInternal(JobTaskPriority priority)
    {
        return priority switch
        {
            JobTaskPriority.None => Contracts.JobTaskPriority.Unspecified,
            JobTaskPriority.Low => Contracts.JobTaskPriority.Low,
            JobTaskPriority.Medium => Contracts.JobTaskPriority.Medium,
            JobTaskPriority.High => Contracts.JobTaskPriority.High,
            JobTaskPriority.Critical => Contracts.JobTaskPriority.Critical,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null),
        };
    }
}