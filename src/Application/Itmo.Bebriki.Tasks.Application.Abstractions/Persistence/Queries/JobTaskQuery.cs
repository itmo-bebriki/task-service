using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record JobTaskQuery(
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
    [RequiredValue] int PageSize);