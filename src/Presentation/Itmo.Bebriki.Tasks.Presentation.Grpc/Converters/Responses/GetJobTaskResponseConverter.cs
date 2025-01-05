using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos;
using JobTaskDto = Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos.JobTaskDto;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Responses;

internal static class GetJobTaskResponseConverter
{
    internal static GetJobTaskResponse FromInternal(JobTaskDto dto)
    {
        return new GetJobTaskResponse
        {
            JobTask = JobTaskDtoConverter.FromInternal(dto),
        };
    }
}