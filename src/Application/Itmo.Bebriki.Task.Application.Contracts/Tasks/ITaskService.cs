using Itmo.Bebriki.Task.Application.Contracts.Tasks.Operations;

namespace Itmo.Bebriki.Task.Application.Contracts.Tasks;

public interface ITaskService
{
    Task<GetTaskCommand.Result> GetTaskByIdAsync(
        GetTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<CreateTaskCommand.Result> CreateTaskAsync(
        CreateTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetTaskStateCommand.Result> SetTaskStateAsync(
        SetTaskStateCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetTaskPriorityCommand.Result> SetTaskPriorityAsync(
        SetTaskPriorityCommand.Request request,
        CancellationToken cancellationToken);

    Task<AddDependOnTaskCommand.Result> AddDependOnTaskAsync(
        AddDependOnTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetTaskDeadlineCommand.Result> SetTaskDeadlineAsync(
        SetTaskDeadlineCommand.Request request,
        CancellationToken cancellationToken);
    
    Task<SetTaskTitleCommand.Result> SetTaskTitleAsync(
        SetTaskTitleCommand.Request request,
        CancellationToken cancellationToken);
    
    Task<SetTaskDescriptionCommand.Result> SetTaskDescriptionAsync(
        SetTaskDescriptionCommand.Request request,
        CancellationToken cancellationToken);
}