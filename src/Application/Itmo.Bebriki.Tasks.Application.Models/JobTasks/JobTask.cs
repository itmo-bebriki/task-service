namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public sealed record JobTask
{
    internal JobTask() { }

    public long Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public long AssigneeId { get; init; }

    public JobTaskState State { get; init; }

    public JobTaskPriority Priority { get; init; }

    public IReadOnlySet<long> DependOnJobTaskIds { get; init; } = new HashSet<long>();

    public DateTimeOffset DeadLine { get; init; }

    public bool IsAgreed { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }
}