using Itmo.Bebriki.Tasks.Presentation.Grpc.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationGrpc(this IServiceCollection collection)
    {
        collection.AddGrpc(grpc =>
        {
            grpc.Interceptors.Add<ExceptionHandlingInterceptor>();
            grpc.Interceptors.Add<ValidationInterceptor>();
        });
        collection.AddGrpcReflection();

        return collection;
    }
}