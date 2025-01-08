using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;

internal static class GetJobTaskRequestConverter
{
    internal static GetJobTaskCommand ToInternal(GetJobTaskRequest request)
    {
        return new GetJobTaskCommand(request.JobTaskId);
    }
}