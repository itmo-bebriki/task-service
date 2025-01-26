using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.ProducerHandlers.JobTaskSubmission;

internal sealed class CreateJobTaskSubmissionHandler : IEventHandler<CreateJobTaskEvent>
{
    private readonly IKafkaMessageProducer<JobTaskSubmissionKey, JobTaskSubmissionValue> _producer;

    public CreateJobTaskSubmissionHandler(IKafkaMessageProducer<JobTaskSubmissionKey, JobTaskSubmissionValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(CreateJobTaskEvent evt, CancellationToken cancellationToken)
    {
        var key = new JobTaskSubmissionKey { JobTaskId = evt.JobTaskId };
        JobTaskSubmissionValue value = JobTaskSubmissionConverter.ToValue(evt);

        var message = new KafkaProducerMessage<JobTaskSubmissionKey, JobTaskSubmissionValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}