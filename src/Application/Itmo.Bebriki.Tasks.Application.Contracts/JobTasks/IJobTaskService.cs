using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Commands;
using Itmo.Bebriki.Tasks.Application.Contracts.JobTasks.Dtos;

namespace Itmo.Bebriki.Tasks.Application.Contracts.JobTasks;

public interface IJobTaskService
{
    Task<PagedJobTaskDtos> QueryJobTasksAsync(
        QueryJobTaskCommand command,
        CancellationToken cancellationToken);

    Task<long> CreateJobTaskAsync(
        CreateJobTaskCommand command,
        CancellationToken cancellationToken);

    Task UpdateJobTaskAsync(
        UpdateJobTaskCommand command,
        CancellationToken cancellationToken);

    Task AddJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken);

    Task RemoveJobTaskDependenciesAsync(
        SetJobTaskDependenciesCommand command,
        CancellationToken cancellationToken);
}