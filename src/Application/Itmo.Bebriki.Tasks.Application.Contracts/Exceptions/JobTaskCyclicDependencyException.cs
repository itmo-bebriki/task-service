namespace Itmo.Bebriki.Tasks.Application.Contracts.Exceptions;

public sealed class JobTaskCyclicDependencyException : Exception
{
    public JobTaskCyclicDependencyException() { }

    public JobTaskCyclicDependencyException(string message) : base(message) { }

    public JobTaskCyclicDependencyException(string message, Exception inner) : base(message, inner) { }
}