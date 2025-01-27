using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;
using Itmo.Dev.Platform.Kafka.Consumer;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.ConsumerHandlers;

internal sealed class JobTaskDecisionConsumerHandler
    : IKafkaConsumerHandler<JobTaskDecisionKey, JobTaskDecisionValue>
{
    private readonly IJobTaskService _jobTaskService;
    private readonly ILogger<JobTaskDecisionConsumerHandler> _logger;

    public JobTaskDecisionConsumerHandler(
        IJobTaskService jobTaskService,
        ILogger<JobTaskDecisionConsumerHandler> logger)
    {
        _jobTaskService = jobTaskService;
        _logger = logger;
    }

    public async ValueTask HandleAsync(
        IEnumerable<IKafkaConsumerMessage<JobTaskDecisionKey, JobTaskDecisionValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaConsumerMessage<JobTaskDecisionKey, JobTaskDecisionValue> message in messages)
        {
            UpdateJobTaskCommand internalCommand = JobTaskDecisionConverter.ToInternal(message.Key, message.Value);
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