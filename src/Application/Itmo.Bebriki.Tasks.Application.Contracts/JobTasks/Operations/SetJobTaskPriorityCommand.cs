using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class SetJobTaskPriorityCommand
{
    public readonly record struct Request(long TaskId, JobTaskPriority Priority);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;

        public sealed record TaskNewPriorityInvalid : Result;
    }
}