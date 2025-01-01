namespace Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

public static class CreateTaskCommand
{
    //TODO
    public readonly record struct Request();

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(long Id) : Result;

        public sealed record Failure : Result;
    }
}