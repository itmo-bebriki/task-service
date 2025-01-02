namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public sealed class SetJobTaskDescriptionCommand
{
    public readonly record struct Request(long TaskId, string? Description);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
    }
}