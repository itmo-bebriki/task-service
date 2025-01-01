using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Task.Presentation.Kafka.Extensions;

public static class EventsConfigurationBuilderExtensions
{
    public static IEventsConfigurationBuilder AddPresentationKafkaHandlers(this IEventsConfigurationBuilder builder)
        => builder.AddHandlersFromAssemblyContaining<IAssemblyMarker>();
}