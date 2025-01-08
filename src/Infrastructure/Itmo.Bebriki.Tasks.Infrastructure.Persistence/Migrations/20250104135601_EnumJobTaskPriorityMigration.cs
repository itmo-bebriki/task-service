using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Tasks.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250104135601, "enum job task priority")]
internal sealed class EnumJobTaskPriorityMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create type job_task_priority as enum (
            'none',
            'low',
            'medium',
            'high',
            'critical'
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop type job_task_priority;
        """;
    }
}