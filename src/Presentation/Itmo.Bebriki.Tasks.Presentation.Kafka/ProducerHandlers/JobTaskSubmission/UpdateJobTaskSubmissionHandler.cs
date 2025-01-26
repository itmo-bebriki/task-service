using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Events;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.Converters;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;
using JobTaskState = Itmo.Bebriki.Tasks.Application.Models.JobTasks.JobTaskState;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.ProducerHandlers.JobTaskSubmission;

internal sealed class UpdateJobTaskSubmissionHandler : IEventHandler<UpdateJobTaskEvent>
{
    private readonly IKafkaMessageProducer<JobTaskSubmissionKey, JobTaskSubmissionValue> _producer;

    public UpdateJobTaskSubmissionHandler(IKafkaMessageProducer<JobTaskSubmissionKey, JobTaskSubmissionValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(UpdateJobTaskEvent evt, CancellationToken cancellationToken)
    {
        if (evt.State is not null && evt.State != JobTaskState.PendingApproval)
        {
            return;
        }

        var key = new JobTaskSubmissionKey { JobTaskId = evt.JobTaskId };
        JobTaskSubmissionValue value = JobTaskSubmissionConverter.ToValue(evt);

        var message = new KafkaProducerMessage<JobTaskSubmissionKey, JobTaskSubmissionValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}