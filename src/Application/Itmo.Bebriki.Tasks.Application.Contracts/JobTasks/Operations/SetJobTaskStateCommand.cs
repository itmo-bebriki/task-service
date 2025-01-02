using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class SetJobTaskStateCommand
{
    public readonly record struct Request(long TaskId, JobTaskState State);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;

        public sealed record TaskNewStateInvalid : Result;
    }
}