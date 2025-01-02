using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class CreateJobTaskCommand
{
    public readonly record struct Request(
        string Title,
        string? Description,
        long AssigneeId,
        JobTaskState State,
        JobTaskPriority Priority,
        IReadOnlyList<long> DependOnTasks,
        DateTimeOffset? DeadLine);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(long Id) : Result;

        public sealed record Failure : Result;
    }
}