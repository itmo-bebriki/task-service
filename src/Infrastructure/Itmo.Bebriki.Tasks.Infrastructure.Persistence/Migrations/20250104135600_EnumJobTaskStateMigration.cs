using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250104135600, "enum job task state")]
public sealed class EnumJobTaskState : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create type job_task_state as enum (
            'none',
            'backlog',
            'todo',
            'in_progress',
            'in_review',
            'done',
            'closed'
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop type job_task_state;
        """;
    }
}