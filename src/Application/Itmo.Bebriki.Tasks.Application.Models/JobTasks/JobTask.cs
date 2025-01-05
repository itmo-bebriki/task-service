namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public sealed record JobTask
{
    internal JobTask() { }

    public long Id { get; init; }

    required public string Title { get; init; }

    public string? Description { get; init; }

    required public long AssigneeId { get; init; }

    required public JobTaskState State { get; init; }

    required public JobTaskPriority Priority { get; init; }

    public IReadOnlySet<long> DependOnJobTaskIds { get; init; } = new HashSet<long>();

    public DateTimeOffset? DeadLine { get; init; }

    public bool IsAgreed { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }
}