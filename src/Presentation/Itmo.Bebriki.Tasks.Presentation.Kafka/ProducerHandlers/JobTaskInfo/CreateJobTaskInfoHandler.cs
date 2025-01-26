using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.ProducerHandlers.JobTaskInfo;

internal sealed class CreateJobTaskInfoHandler : IEventHandler<CreateJobTaskEvent>
{
    private readonly IKafkaMessageProducer<JobTaskInfoKey, JobTaskInfoValue> _producer;

    public CreateJobTaskInfoHandler(IKafkaMessageProducer<JobTaskInfoKey, JobTaskInfoValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(CreateJobTaskEvent evt, CancellationToken cancellationToken)
    {
        var key = new JobTaskInfoKey { JobTaskId = evt.JobTaskId };
        JobTaskInfoValue value = JobTaskInfoConverter.ToValue(evt);

        var message = new KafkaProducerMessage<JobTaskInfoKey, JobTaskInfoValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}