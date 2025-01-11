using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Tasks.Presentation.Kafka.Extensions;

public static class EventsConfigurationBuilderExtensions
{
    public static IEventsConfigurationBuilder AddPresentationKafkaHandlers(this IEventsConfigurationBuilder builder)
    {
        return builder.AddHandlersFromAssemblyContaining<IAssemblyMarker>();
    }
}