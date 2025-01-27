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
            jt.updated_at,
            coalesce(ad.depends_on_ids, '{}') as depends_on_ids
        from job_tasks as jt
        left join aggregated_dependencies ad
            on jt.job_task_id = ad.job_task_id
        where (:cursor is null or jt.job_task_id > :cursor)
            and (cardinality(:job_task_ids) = 0 or jt.job_task_id = any(:job_task_ids))
            and (cardinality(:assignee_ids) = 0 or jt.assignee_id = any(:assignee_ids))
            and (cardinality(:states) = 0 or jt.state = any(:states))
            and (cardinality(:priorities) = 0 or jt.priority = any(:priorities))
            and (cardinality(:depends_on_task_ids) = 0
                    or jt.job_task_id in (select d.job_task_id from dependency_tree as d))
            and (:from_deadline is null or jt.dead_line >= :from_deadline)
            and (:to_deadline is null or jt.dead_line <= :to_deadline)
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
                deadline: reader.GetFieldValue<DateTimeOffset>("dead_line"),
                updatedAt: reader.GetFieldValue<DateTimeOffset>("updated_at"));
        }
    }

    public async IAsyncEnumerable<long> AddAsync(
        IReadOnlyCollection<JobTask> jobTasks,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string insertTasksSql =
        """
        insert into job_tasks as jt 
            (title, description, assignee_id, state, priority, dead_line, updated_at)
        select 
            source.title,
            source.description,
            source.assignee_id,
            source.state,
            source.priority,
            source.dead_line,
            source.updated_at
        from unnest(
            :titles,
            :descriptions,
            :assignee_ids,
            :states,
            :priorities,
            :dead_lines,
            :updated_ats
        ) as source (
            title,
            description,
            assignee_id,
            state,
            priority,
            dead_line,
            updated_at
        )
        returning jt.job_task_id;
        """;

        const string insertDependenciesSql =
        """
        insert into job_task_dependencies (job_task_id, depends_on_job_task_id)
        select :job_task_id, unnest(:depends_on_job_task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand insertTasksCommand = connection.CreateCommand(insertTasksSql)
            .AddParameter("titles", jobTasks.Select(t => t.Title))
            .AddParameter("descriptions", jobTasks.Select(t => t.Description))
            .AddParameter("assignee_ids", jobTasks.Select(t => t.AssigneeId))
            .AddParameter("states", jobTasks.Select(t => t.State))
            .AddParameter("priorities", jobTasks.Select(t => t.Priority))
            .AddParameter("dead_lines", jobTasks.Select(t => t.DeadLine))
            .AddParameter("updated_ats", jobTasks.Select(t => t.UpdatedAt));

        var newJobTaskIds = new List<long>();

        await using (DbDataReader reader = await insertTasksCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                newJobTaskIds.Add(reader.GetInt64(0));
            }
        }

        for (int i = 0; i < jobTasks.Count; i++)
        {
            JobTask jobTask = jobTasks.ElementAt(i);

            if (!jobTask.DependOnJobTaskIds.Any())
            {
                continue;
            }

            await using IPersistenceCommand insertDependenciesCommand = connection.CreateCommand(insertDependenciesSql)
                .AddParameter("job_task_id", newJobTaskIds[i])
                .AddParameter("depends_on_job_task_ids", jobTask.DependOnJobTaskIds.ToArray());

            await insertDependenciesCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (long jobTaskId in newJobTaskIds)
        {
            yield return jobTaskId;
        }
    }

    public async Task UpdateAsync(IReadOnlyCollection<JobTask> jobTasks, CancellationToken cancellationToken)
    {
        const string sql =
        """
        update job_tasks as jt
        set
            title = source.title,
            description = source.description,
            assignee_id = source.assignee_id,
            state = source.state,
            priority = source.priority,
            dead_line = source.dead_line,
            updated_at = source.updated_at
        from unnest(
            :job_task_ids,
            :titles,
            :descriptions,
            :assignee_ids,
            :states,
            :priorities,
            :dead_lines,
            :updated_ats
        ) as source (
            job_task_id,
            title,
            description,
            assignee_id,
            state,
            priority,
            dead_line,
            updated_at
        )
        where jt.job_task_id = source.job_task_id;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("job_task_ids", jobTasks.Select(t => t.Id))
            .AddParameter("titles", jobTasks.Select(t => t.Title))
            .AddParameter("descriptions", jobTasks.Select(t => t.Description))
            .AddParameter("assignee_ids", jobTasks.Select(t => t.AssigneeId))
            .AddParameter("states", jobTasks.Select(t => t.State))
            .AddParameter("priorities", jobTasks.Select(t => t.Priority))
            .AddParameter("dead_lines", jobTasks.Select(t => t.DeadLine))
            .AddParameter("updated_ats", jobTasks.Select(t => t.UpdatedAt));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AddDependenciesAsync(JobTaskDependenciesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        insert into job_task_dependencies (job_task_id, depends_on_job_task_id)
        select :job_task_id, unnest(:depends_on_job_task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("job_task_id", query.JobTaskId)
            .AddParameter("depends_on_job_task_ids", query.DependOnIds);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RemoveDependenciesAsync(JobTaskDependenciesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        delete from job_task_dependencies as jtd
        where jtd.job_task_id = :job_task_id
            and jtd.depends_on_job_task_id = any(:depends_on_job_task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("job_task_id", query.JobTaskId)
            .AddParameter("depends_on_job_task_ids", query.DependOnIds);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<JobTask> GetDependentJobTasksAsync(
        DependentJobTaskQuery query,
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
            jt.updated_at,
            coalesce(ad.depends_on_ids, '{}') as depends_on_ids
        from job_tasks as jt
        left join aggregated_dependencies ad
            on jt.job_task_id = ad.job_task_id
        where (cardinality(:depends_on_task_ids) = 0
                    or jt.job_task_id in (select job_task_id from dependency_tree));
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("depends_on_task_ids", query.JobTaskIds.ToArray());

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
                deadline: reader.GetFieldValue<DateTimeOffset>("dead_line"),
                updatedAt: reader.GetFieldValue<DateTimeOffset>("updated_at"));
        }
    }
}