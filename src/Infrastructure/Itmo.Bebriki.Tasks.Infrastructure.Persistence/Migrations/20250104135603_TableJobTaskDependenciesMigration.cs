using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250104135603, "table job task dependencies")]
public sealed class TableJobTaskDependenciesMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table job_task_dependencies (
            job_task_id bigint not null references job_tasks(job_task_id) on delete cascade,
            depends_on_job_task_id bigint not null references job_tasks(job_task_id) on delete cascade,
            
            primary key (job_task_id, depends_on_job_task_id)
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table job_task_dependencies;
        """;
    }
}