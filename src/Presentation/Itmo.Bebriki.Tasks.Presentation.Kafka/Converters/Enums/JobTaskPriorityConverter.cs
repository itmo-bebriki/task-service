using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

internal static class JobTaskPriorityConverter
{
    internal static JobTaskPriority ToInternal(Tasks.Kafka.Contracts.JobTaskPriority priority)
    {
        return priority switch
        {
            Tasks.Kafka.Contracts.JobTaskPriority.Unspecified => JobTaskPriority.None,
            Tasks.Kafka.Contracts.JobTaskPriority.Low => JobTaskPriority.Low,
            Tasks.Kafka.Contracts.JobTaskPriority.Medium => JobTaskPriority.Medium,
            Tasks.Kafka.Contracts.JobTaskPriority.High => JobTaskPriority.High,
            Tasks.Kafka.Contracts.JobTaskPriority.Critical => JobTaskPriority.Critical,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null),
        };
    }

    internal static Tasks.Kafka.Contracts.JobTaskPriority FromInternal(JobTaskPriority priority)
    {
        return priority switch
        {
            JobTaskPriority.None => Tasks.Kafka.Contracts.JobTaskPriority.Unspecified,
            JobTaskPriority.Low => Tasks.Kafka.Contracts.JobTaskPriority.Low,
            JobTaskPriority.Medium => Tasks.Kafka.Contracts.JobTaskPriority.Medium,
            JobTaskPriority.High => Tasks.Kafka.Contracts.JobTaskPriority.High,
            JobTaskPriority.Critical => Tasks.Kafka.Contracts.JobTaskPriority.Critical,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null),
        };
    }
}