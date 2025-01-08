using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Contracts;
using Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Tasks.Presentation.Grpc.Converters.Requests;

internal static class QueryJobTaskRequestConverter
{
    internal static QueryJobTaskCommand ToInternal(QueryJobTaskRequest request)
    {
        return new QueryJobTaskCommand(
            JobTaskIds: request.JobTaskIds?.ToArray() ?? [],
            AssigneeIds: request.AssigneeIds?.ToArray() ?? [],
            States: request.States?.Select(JobTaskStateConverter.ToInternal).ToArray() ?? [],
            Priorities: request.Priorities?.Select(JobTaskPriorityConverter.ToInternal).ToArray() ?? [],
            DependsOnTaskIds: request.DependsOnTaskIds?.ToArray() ?? [],
            FromDeadline: request.FromDeadline?.ToDateTimeOffset(),
            ToDeadline: request.ToDeadline?.ToDateTimeOffset(),
            IsAgreed: request.IsAgreed,
            FromUpdatedAt: request.FromUpdatedAt?.ToDateTimeOffset(),
            ToUpdatedAt: request.ToUpdatedAt?.ToDateTimeOffset(),
            Cursor: request.Cursor,
            PageSize: request.PageSize);
    }
}