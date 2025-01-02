namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class AddDependOnJobTaskCommand
{
    public readonly record struct Request(long TaskId, long DependsOnTaskId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}