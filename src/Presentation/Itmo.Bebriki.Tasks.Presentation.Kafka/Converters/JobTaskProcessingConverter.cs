using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;

internal static class JobTaskProcessingConverter
{
    internal static UpdateJobTaskCommand ToInternal(JobTaskProcessingKey key, JobTaskProcessingValue value)
    {
        return new UpdateJobTaskCommand(
            JobTaskId: key.JobTaskId,
            State: JobTaskStateConverter.ToInternal(value.State));
    }
}