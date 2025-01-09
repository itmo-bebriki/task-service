using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;

namespace Itmo.Bebriki.Tasks.Application.Converters.Commands;

internal static class SetJobTaskDependenciesCommandConverter
{
    internal static JobTaskDependenciesQuery ToQuery(SetJobTaskDependenciesCommand command)
    {
        return JobTaskDependenciesQuery.Build(builder => builder
            .WithJobTaskId(command.JobTaskId)
            .WithDependOnIds(command.DependOnJobTaskIds));
    }
}