using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Responses;

internal static class QueryJobTaskResponseConverter
{
    internal static QueryJobTaskResponse FromInternal(PagedJobTaskDtos dtos)
    {
        return new QueryJobTaskResponse
        {
            Cursor = dtos.Cursor,
            JobTasks = { dtos.JobTaskDtos.Select(JobTaskDtoConverter.FromInternal) },
        };
    }
}