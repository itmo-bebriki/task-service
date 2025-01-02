using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public static class GetJobTaskCommand
{
    public readonly record struct Request(long TaskId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(JobTaskDto JobTaskDto) : Result;

        public sealed record TaskNotFound : Result;
    }
}