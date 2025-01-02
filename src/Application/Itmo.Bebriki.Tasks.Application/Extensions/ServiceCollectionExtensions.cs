using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.JobTasks;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Tasks.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IJobTaskService, JobJobTaskService>();

        return collection;
    }
}