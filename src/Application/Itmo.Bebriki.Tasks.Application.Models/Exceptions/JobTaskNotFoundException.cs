namespace Itmo.Bebriki.Tasks.Application.Models.Exceptions;

public sealed class JobTaskNotFoundException : Exception
{
    public JobTaskNotFoundException() { }

    public JobTaskNotFoundException(string message) : base(message) { }

    public JobTaskNotFoundException(string message, Exception inner) : base(message, inner) { }
}