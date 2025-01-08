using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;

internal static class CreateJobTaskRequestConverter
{
    internal static CreateJobTaskCommand ToInternal(CreateJobTaskRequest request)
    {
        return new CreateJobTaskCommand(
            Title: request.Title,
            Description: request.Description,
            AssigneeId: request.AssigneeId,
            Priority: JobTaskPriorityConverter.ToInternal(request.Priority),
            DependOnTasks: request.DependOnTaskIds.ToHashSet(),
            DeadLine: request.DeadLine.ToDateTimeOffset());
    }
}