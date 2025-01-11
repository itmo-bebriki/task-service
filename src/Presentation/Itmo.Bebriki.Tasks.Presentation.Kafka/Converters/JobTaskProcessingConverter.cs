using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskProcessingConverter
{
    internal static UpdateJobTaskCommand ToInternal(JobTaskProcessingKey key, JobTaskProcessingValue value)
    {
        return new UpdateJobTaskCommand(
            JobTaskId: key.JobTaskId,
            IsAgreed: value.IsAgreed);
    }
}