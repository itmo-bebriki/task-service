namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class SetJobTaskTitleCommand
{
    public readonly record struct Request(long TaskId, string Title);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}