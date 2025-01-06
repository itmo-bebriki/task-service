using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public sealed record CreateJobTaskCommand(
    string Title,
    string Description,
    long AssigneeId,
    JobTaskPriority Priority,
    IReadOnlySet<long> DependOnTasks,
    DateTimeOffset DeadLine);