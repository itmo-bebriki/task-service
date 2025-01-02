using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;

public interface IJobTaskService
{
    Task<GetJobTaskCommand.Result> GetTaskByIdAsync(
        GetJobTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<CreateJobTaskCommand.Result> CreateTaskAsync(
        CreateJobTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetJobTaskStateCommand.Result> SetTaskStateAsync(
        SetJobTaskStateCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetJobTaskPriorityCommand.Result> SetTaskPriorityAsync(
        SetJobTaskPriorityCommand.Request request,
        CancellationToken cancellationToken);

    Task<AddDependOnJobTaskCommand.Result> AddDependOnTaskAsync(
        AddDependOnJobTaskCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetJobTaskDeadlineCommand.Result> SetTaskDeadlineAsync(
        SetJobTaskDeadlineCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetJobTaskTitleCommand.Result> SetTaskTitleAsync(
        SetJobTaskTitleCommand.Request request,
        CancellationToken cancellationToken);

    Task<SetJobTaskDescriptionCommand.Result> SetTaskDescriptionAsync(
        SetJobTaskDescriptionCommand.Request request,
        CancellationToken cancellationToken);
}