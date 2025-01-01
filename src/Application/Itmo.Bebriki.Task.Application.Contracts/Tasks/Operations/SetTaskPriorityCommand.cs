using Itmo.Bebriki.Task.Application.Models.Tasks;

namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class SetTaskPriorityCommand
{
    public readonly record struct Request(long TaskId, TaskPriority Priority);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record TaskNotFound : Result;
        
        public sealed record TaskNewPriorityInvalid : Result;
    }
}