using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Tasks.Application.Abstractions.Persistence.Repositories;
using Itmo.Bebriki.Tasks.Application.Models.JobTasks;
using Itmo.Dev.Platform.Persistence.Abstractions.Commands;
using Itmo.Dev.Platform.Persistence.Abstractions.Connections;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Repositories;

internal sealed class JobTaskRepository : IJobTaskRepository
{
    private readonly IPersistenceConnectionProvider _connectionProvider;

    public JobTaskRepository(IPersistenceConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async IAsyncEnumerable<JobTask> QueryAsync(
        JobTaskQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql =
        """
        with recursive dependency_tree as (
            select
                jtd.job_task_id
            from job_task_dependencies as jtd
            where jtd.depends_on_job_task_id = any(:depends_on_task_ids)
        
            union all
        
            select
                jtd1.job_task_id
            from job_task_dependencies as jtd1
            join dependency_tree dt on jtd1.depends_on_job_task_id = dt.job_task_id
        ),
        aggregated_dependencies as (
            select
                jtd.job_task_id,
                array_agg(jtd.depends_on_job_task_id) as depends_on_ids
            from job_task_dependencies as jtd
            group by jtd.job_task_id
        )
        select
            jt.job_task_id,
            jt.title,
            jt.description,
            jt.assignee_id,
            jt.state,
            jt.priority,
            jt.dead_line,
            jt.is_agreed,
            jt.updated_at,
            coalesce(ad.depends_on_ids, '{}') as depends_on_ids
        from job_tasks as jt
        left join aggregated_dependencies ad
            on jt.job_task_id = ad.job_task_id
        where (:cursor is null or jt.job_task_id > :cursor)
            and (cardinality(:job_task_ids) = 0 or jt.job_task_id = any(:job_task_ids))
            and (cardinality(:assignee_ids) = 0 or jt.assignee_id = any(:assignee_ids))
            and (cardinality(:states) = 0 or jt.state = any(:states))
            and (cardinality(:priorities) = 0 or jt.priority = any(:priority))
            and (cardinality(:depends_on_task_ids) = 0
                    or jt.job_task_id in (select job_task_id from dependency_tree))
            and (:from_deadline is null or jt.dead_line >= :from_deadline)
            and (:to_deadline is null or jt.dead_line <= :to_deadline)
            and (:is_agreed is null or jt.is_agreed = :is_agreed)
            and (:from_updated_at is null or jt.updated_at >= :from_updated_at)
            and (:to_updated_at is null or jt.updated_at <= :to_updated_at)
        order by jt.job_task_id
        limit :page_size;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("job_task_ids", query.JobTaskIds)
            .AddParameter("assignee_ids", query.AssigneeIds)
            .AddParameter("states", query.States)
            .AddParameter("priorities", query.Priorities)
            .AddParameter("depends_on_task_ids", query.DependsOnTaskIds)
            .AddParameter("from_deadline", query.FromDeadline)
            .AddParameter("to_deadline", query.ToDeadline)
            .AddParameter("is_agreed", query.IsAgreed)
            .AddParameter("from_updated_at", query.FromUpdatedAt)
            .AddParameter("to_updated_at", query.ToUpdatedAt)
            .AddParameter("cursor", query.Cursor)
            .AddParameter("page_size", query.PageSize);

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return JobTaskFactory.CreateNew(
                id: reader.GetInt64("job_task_id"),
                title: reader.GetString("title"),
                description: reader.GetString("description"),
                assigneeId: reader.GetInt64("assignee_id"),
                state: reader.GetFieldValue<JobTaskState>("state"),
                priority: reader.GetFieldValue<JobTaskPriority>("priority"),
                dependsOnIds: reader.GetFieldValue<long[]>("depends_on_ids"),
                deadline: reader.GetFieldValue<DateTimeOffset?>("deadline"),
                isAgreed: reader.GetBoolean("is_agreed"),
                updatedAt: reader.GetFieldValue<DateTimeOffset>("updated_at"));
        }
    }

    public IAsyncEnumerable<long> AddAsync(IReadOnlyCollection<JobTask> jobTasks, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IReadOnlyCollection<JobTask> jobTasks, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}