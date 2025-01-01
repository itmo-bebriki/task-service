namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class SetTaskAssigneeCommand
{
    public readonly record struct Request(long TaskId, long AssigneeId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}