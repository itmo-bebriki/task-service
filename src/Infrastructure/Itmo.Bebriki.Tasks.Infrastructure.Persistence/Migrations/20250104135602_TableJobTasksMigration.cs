using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250104135602, "table job task")]
public sealed class TableJobTasksMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table job_tasks (
            job_task_id bigint primary key generated always as identity,
            title text not null,
            description text not null,
            assignee_id bigint not null,
            state job_task_state not null,
            priority job_task_priority not null,
            dead_line timestamp with time zone not null,
            is_agreed boolean not null default false,
            updated_at timestamp with time zone not null
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table job_tasks;
        """;
    }
}