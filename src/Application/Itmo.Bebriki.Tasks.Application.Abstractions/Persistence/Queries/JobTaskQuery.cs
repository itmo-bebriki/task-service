using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record JobTaskQuery(
    long[] JobTaskIds,
    long[] AssigneeIds,
    JobTaskState[] States,
    JobTaskPriority[] Priorities,
    DateTimeOffset? Deadline,
    [RequiredValue] int PageSize,
    long Cursor);