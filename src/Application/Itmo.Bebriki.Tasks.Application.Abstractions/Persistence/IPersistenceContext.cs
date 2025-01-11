using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;

namespace Itmo.Bebriki.Tasks.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    IJobTaskRepository JobTasks { get; }
}