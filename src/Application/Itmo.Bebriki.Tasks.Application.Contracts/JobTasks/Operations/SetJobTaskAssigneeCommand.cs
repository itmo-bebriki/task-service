namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class SetJobTaskAssigneeCommand
{
    public readonly record struct Request(long TaskId, long AssigneeId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}