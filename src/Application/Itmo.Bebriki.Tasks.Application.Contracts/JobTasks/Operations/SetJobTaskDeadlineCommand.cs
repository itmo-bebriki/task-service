namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class SetJobTaskDeadlineCommand
{
    public readonly record struct Request(long TaskId, DateTime Deadline);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;

        public sealed record TaskInvalidDeadline : Result;
    }
}