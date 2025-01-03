using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

public sealed record UpdateJobTaskCommand(
    long JobTaskId,
    string? Title = null,
    string? Description = null,
    long? AssigneeId = null,
    JobTaskState? State = null,
    JobTaskPriority? Priority = null,
    DateTimeOffset? DeadLine = null,
    long? DependOnTaskId = null)
{
    // public abstract record Result
    // {
    //     private Result() { }
    //
    //     public sealed record Success : Result;
    //
    //     public sealed record TaskNotFound : Result;
    //
    //     public sealed record TaskInvalidDeadline : Result;
    //
    //     public sealed record TaskPriorityInvalid : Result;
    //
    //     public sealed record TaskStateInvalid : Result;
    //
    //     public sealed record InvalidOperation(string Message) : Result;
    // }
}