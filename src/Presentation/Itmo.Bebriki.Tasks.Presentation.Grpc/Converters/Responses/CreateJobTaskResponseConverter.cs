using Itmo.Bebriki.Tasks.Contracts;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Responses;

internal static class CreateJobTaskResponseConverter
{
    internal static CreateJobTaskResponse FromInternal(long jobTaskId)
    {
        return new CreateJobTaskResponse
        {
            JobTaskId = jobTaskId,
        };
    }
}