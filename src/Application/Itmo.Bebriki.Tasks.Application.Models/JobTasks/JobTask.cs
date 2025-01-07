namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public sealed record JobTask
{
    internal JobTask() { }

    public long Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required long AssigneeId { get; init; }

    public required JobTaskState State { get; init; }

    public required JobTaskPriority Priority { get; init; }

    public IReadOnlySet<long> DependOnJobTaskIds { get; init; } = new HashSet<long>();

    public required DateTimeOffset DeadLine { get; init; }

    public bool IsAgreed { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }
}