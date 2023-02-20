using System.ComponentModel;

namespace CRB.TPM.TaskScheduler.Abstractions.Quartz;

public enum QuartzProvider
{
    [Description("SqlServer")]
    SqlServer,
    [Description("MySql")]
    MySql,
    [Description("SQLite-Microsoft")]
    SQLite,
    [Description("OracleODP")]
    Oracle,
    [Description("Npgsql")]
    PostgreSQL
}
