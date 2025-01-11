using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

public sealed record JobTaskDto(
    long Id,
    string Title,
    string Description,
    long AssigneeId,
    JobTaskState State,
    JobTaskPriority Priority,
    IReadOnlySet<long> DependOnTasks,
    DateTimeOffset DeadLine,
    bool IsAgreed,
    DateTimeOffset UpdatedAt);