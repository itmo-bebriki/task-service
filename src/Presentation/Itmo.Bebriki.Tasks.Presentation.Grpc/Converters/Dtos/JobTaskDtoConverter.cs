using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos;

internal static class JobTaskDtoConverter
{
    internal static Contracts.JobTaskDto FromInternal(JobTaskDto internalDto)
    {
        return new Contracts.JobTaskDto
        {
            JobTaskId = internalDto.Id,
            Title = internalDto.Title,
            Description = internalDto.Description,
            AssigneeId = internalDto.AssigneeId,
            State = JobTaskStateConverter.FromInternal(internalDto.State),
            Priority = JobTaskPriorityConverter.FromInternal(internalDto.Priority),
            DependOnTaskIds = { internalDto.DependOnTasks.ToArray() },
            DeadLine = internalDto.DeadLine.ToTimestamp(),
            IsAgreed = internalDto.IsAgreed,
            UpdatedAt = internalDto.UpdatedAt.ToTimestamp(),
        };
    }
}