namespace Itmo.Bebriki.Tasks.Application.Contracts.Exceptions;

public sealed class JobTaskUpdateException : Exception
{
    public JobTaskUpdateException() { }

    public JobTaskUpdateException(string message) : base(message) { }

    public JobTaskUpdateException(string message, Exception inner) : base(message, inner) { }
}