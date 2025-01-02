using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Operations;

namespace Itmo.Bebriki.Tasks.Application.JobTasks;

public sealed class JobJobTaskService : IJobTaskService
{
    public Task<GetJobTaskCommand.Result> GetTaskByIdAsync(GetJobTaskCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CreateJobTaskCommand.Result> CreateTaskAsync(CreateJobTaskCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SetJobTaskStateCommand.Result> SetTaskStateAsync(SetJobTaskStateCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SetJobTaskPriorityCommand.Result> SetTaskPriorityAsync(SetJobTaskPriorityCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AddDependOnJobTaskCommand.Result> AddDependOnTaskAsync(AddDependOnJobTaskCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SetJobTaskDeadlineCommand.Result> SetTaskDeadlineAsync(SetJobTaskDeadlineCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SetJobTaskTitleCommand.Result> SetTaskTitleAsync(SetJobTaskTitleCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SetJobTaskDescriptionCommand.Result> SetTaskDescriptionAsync(SetJobTaskDescriptionCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}