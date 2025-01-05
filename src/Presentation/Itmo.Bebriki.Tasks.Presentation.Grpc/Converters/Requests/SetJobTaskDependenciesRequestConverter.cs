using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
using Itmo.Bebriki.Tasks.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;

internal static class SetJobTaskDependenciesRequestConverter
{
    internal static SetJobTaskDependenciesCommand ToInternal(SetJobTaskDependenciesRequest request)
    {
        return new SetJobTaskDependenciesCommand(
            JobTaskId: request.JobTaskId,
            DependOnJobTaskIds: request.DependOnTaskIds.ToHashSet());
    }
}