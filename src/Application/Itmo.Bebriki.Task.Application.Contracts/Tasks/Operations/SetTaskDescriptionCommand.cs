namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public sealed class SetTaskDescriptionCommand
{
    public readonly record struct Request(long TaskId, string? Description);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}