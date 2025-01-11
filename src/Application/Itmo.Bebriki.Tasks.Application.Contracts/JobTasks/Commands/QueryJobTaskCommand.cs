using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;

public sealed record QueryJobTaskCommand(
    long[] JobTaskIds,
    long[] AssigneeIds,
    JobTaskState[] States,
    JobTaskPriority[] Priorities,
    long[] DependsOnTaskIds,
    DateTimeOffset? FromDeadline,
    DateTimeOffset? ToDeadline,
    bool? IsAgreed,
    DateTimeOffset? FromUpdatedAt,
    DateTimeOffset? ToUpdatedAt,
    long? Cursor,
    int PageSize);