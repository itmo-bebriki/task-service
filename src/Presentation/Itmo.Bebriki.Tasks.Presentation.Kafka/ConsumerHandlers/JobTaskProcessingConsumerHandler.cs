using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;
using Itmo.Dev.Platform.Kafka.Consumer;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.ConsumerHandlers;

internal sealed class JobTaskProcessingConsumerHandler
    : IKafkaConsumerHandler<JobTaskProcessingKey, JobTaskProcessingValue>
{
    private readonly IJobTaskService _jobTaskService;
    private readonly ILogger<JobTaskProcessingConsumerHandler> _logger;

    public JobTaskProcessingConsumerHandler(
        IJobTaskService jobTaskService,
        ILogger<JobTaskProcessingConsumerHandler> logger)
    {
        _jobTaskService = jobTaskService;
        _logger = logger;
    }

    public async ValueTask HandleAsync(
        IEnumerable<IKafkaConsumerMessage<JobTaskProcessingKey, JobTaskProcessingValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaConsumerMessage<JobTaskProcessingKey, JobTaskProcessingValue> message in messages)
        {
            UpdateJobTaskCommand internalCommand = JobTaskProcessingConverter.ToInternal(message.Key, message.Value);
            try
            {
                await _jobTaskService.UpdateJobTaskAsync(internalCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to process Kafka message. Topic: {Topic}, Key: {Key}, Value: {Value}",
                    message.Topic,
                    message.Key,
                    message.Value);
            }
        }
    }
}