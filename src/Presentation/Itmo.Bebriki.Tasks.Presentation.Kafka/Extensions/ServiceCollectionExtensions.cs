using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Kafka.ConsumerHandlers;
using Itmo.Dev.Platform.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationKafka(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        const string configurationSection = "Presentation:Kafka";
        const string consumerKey = "Presentation:Kafka:Consumers";
        const string producerKey = "Presentation:Kafka:Producers";

        collection.AddPlatformKafka(kafka => kafka
            .ConfigureOptions(configuration.GetSection(configurationSection))
            .AddConsumer(consumer => consumer
                .WithKey<JobTaskProcessingKey>()
                .WithValue<JobTaskProcessingValue>()
                .WithConfiguration(configuration.GetSection($"{consumerKey}:JobTaskProcessing"))
                .DeserializeKeyWithProto()
                .DeserializeValueWithProto()
                .HandleWith<JobTaskProcessingConsumerHandler>())
            .AddProducer(producer => producer
                .WithKey<JobTaskInfoKey>()
                .WithValue<JobTaskInfoValue>()
                .WithConfiguration(configuration.GetSection($"{producerKey}:JobTaskInfo"))
                .SerializeKeyWithProto()
                .SerializeValueWithProto()));

        return collection;
    }
}