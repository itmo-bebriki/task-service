using Itmo.Bebriki.Task.Application.Contracts.Tasks.Dtos;

namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class GetTaskCommand
{
    public readonly record struct Request(long TaskId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(TaskDto TaskDto) : Result;

        public sealed record TaskNotFound : Result;
    }
}