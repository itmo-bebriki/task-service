using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;

public interface IJobTaskRepository
{
    IAsyncEnumerable<JobTask> QueryAsync(JobTaskQuery query, CancellationToken cancellationToken);

    Task AddOrUpdateAsync(IReadOnlyCollection<JobTask> tasks, CancellationToken cancellationToken);
}