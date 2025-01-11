using Itmo.Bebriki.Tasks.Presentation.Grpc.Controllers;
using Microsoft.AspNetCore.Builder;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePresentationGrpc(this IApplicationBuilder builder)
    {
        builder.UseEndpoints(routeBuilder =>
        {
            routeBuilder.MapGrpcService<JobTaskController>();
            routeBuilder.MapGrpcReflectionService();
        });

        return builder;
    }
}