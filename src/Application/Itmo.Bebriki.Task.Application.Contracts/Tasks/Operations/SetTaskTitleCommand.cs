namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class SetTaskTitleCommand
{
    public readonly record struct Request(long TaskId, string Title);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}