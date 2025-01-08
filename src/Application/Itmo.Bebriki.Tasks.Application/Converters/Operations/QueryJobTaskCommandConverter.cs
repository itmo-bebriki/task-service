using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;

namespace Itmo.Bebriki.Tasks.Application.Converters.Operations;

internal static class QueryJobTaskCommandConverter
{
    internal static JobTaskQuery ToQuery(QueryJobTaskCommand command)
    {
        return JobTaskQuery.Build(builder => builder
            .WithJobTaskIds(command.JobTaskIds)
            .WithAssigneeIds(command.AssigneeIds)
            .WithStates(command.States)
            .WithPriorities(command.Priorities)
            .WithDependsOnTaskIds(command.DependsOnTaskIds)
            .WithFromDeadline(command.FromDeadline)
            .WithToDeadline(command.ToDeadline)
            .WithIsAgreed(command.IsAgreed)
            .WithFromUpdatedAt(command.FromUpdatedAt)
            .WithToUpdatedAt(command.ToUpdatedAt)
            .WithCursor(command.Cursor)
            .WithPageSize(command.PageSize));
    }
}