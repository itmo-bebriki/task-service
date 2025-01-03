namespace Itmo.Bebriki.Tasks.Application.Models.JobTasks;

public sealed record JobTask
{
    internal JobTask() { }

    public long Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required long AssigneeId { get; init; }

    public required JobTaskState State { get; init; }

    public required JobTaskPriority Priority { get; init; }

    public ISet<long> DependOnTasks { get; init; } = new HashSet<long>();

    public DateTimeOffset? DeadLine { get; init; }

    public bool IsAgreed { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }
}