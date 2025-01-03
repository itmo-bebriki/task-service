using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence;

public class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(IJobTaskRepository jobTasks)
    {
        JobTasks = jobTasks;
    }

    public IJobTaskRepository JobTasks { get; }
}