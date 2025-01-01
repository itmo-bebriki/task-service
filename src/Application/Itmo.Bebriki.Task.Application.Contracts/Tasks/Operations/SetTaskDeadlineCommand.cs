namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class SetTaskDeadlineCommand
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