using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;
using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;

internal static class UpdateJobTaskRequestConverter
{
    internal static UpdateJobTaskCommand ToInternal(UpdateJobTaskRequest request)
    {
        return new UpdateJobTaskCommand(
            JobTaskId: request.JobTaskId,
            Title: request.Title,
            Description: request.Description,
            AssigneeId: request.AssigneeId,
            State: request.State == JobTaskState.Unspecified ? null : JobTaskStateConverter.ToInternal(request.State),
            Priority: request.Priority == JobTaskPriority.Unspecified ? null : JobTaskPriorityConverter.ToInternal(request.Priority),
            DeadLine: request.DeadLine?.ToDateTimeOffset(),
            IsAgreed: request.IsAgreed);
    }
}