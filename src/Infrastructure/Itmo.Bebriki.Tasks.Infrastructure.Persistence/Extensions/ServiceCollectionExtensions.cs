using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;
using Itmo.Bebriki.Tasks.Infrastructure.Persistence.Plugins;
using Itmo.Bebriki.Tasks.Infrastructure.Persistence.Repositories;
using Itmo.Dev.Platform.Persistence.Abstractions.Extensions;
using Itmo.Dev.Platform.Persistence.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection collection)
    {
        collection.AddPlatformPersistence(persistence => persistence
            .UsePostgres(postgres => postgres
                .WithConnectionOptions(b => b.BindConfiguration("Infrastructure:Persistence:Postgres"))
                .WithMigrationsFrom(typeof(IAssemblyMarker).Assembly)
                .WithDataSourcePlugin<MappingPlugin>()));

        collection.AddScoped<IPersistenceContext, PersistenceContext>();

        collection.AddSingleton<IJobTaskRepository, JobTaskRepository>();

        return collection;
    }
}