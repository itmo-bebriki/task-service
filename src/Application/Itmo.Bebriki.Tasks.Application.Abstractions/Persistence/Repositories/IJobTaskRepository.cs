using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;

public interface IJobTaskRepository
{
    IAsyncEnumerable<JobTask> QueryAsync(JobTaskQuery query, CancellationToken cancellationToken);

    IAsyncEnumerable<long> AddAsync(IReadOnlyCollection<JobTask> jobTasks, CancellationToken cancellationToken);

    Task UpdateAsync(IReadOnlyCollection<JobTask> jobTasks, CancellationToken cancellationToken);

    Task AddDependenciesAsync(
        JobTaskDependenciesQuery query,
        CancellationToken cancellationToken);

    Task RemoveDependenciesAsync(
        JobTaskDependenciesQuery query,
        CancellationToken cancellationToken);
}