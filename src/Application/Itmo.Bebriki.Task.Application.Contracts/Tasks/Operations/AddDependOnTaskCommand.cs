namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class AddDependOnTaskCommand
{
    public readonly record struct Request(long TaskId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}