using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.Events;
using Dapper;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Defaults;

public class CreateDatabaseEvent : IDatabaseCreateEvent
{

    /// <summary>
    /// 创建前的事件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnBeforeCreate(DatabaseCreateContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 创建后的事件(这里用来创建表结构)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task OnAfterCreate(DatabaseCreateContext context)
    {
        var sql = new CreateTableSql();
        bool exist;
        using var con = context.DbContext.NewConnection();
        switch (context.DbContext.Options.Provider)
        {
            case DbProvider.SqlServer:
                exist = con.ExecuteScalar<int>("SELECT TOP 1 1 FROM sysobjects WHERE id = OBJECT_ID(N'QRTZ_BLOB_TRIGGERS') AND xtype = 'U';") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.SqlServer)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
                break;
            case DbProvider.MySql:
                exist = con.ExecuteScalar<int>($"SELECT 1 FROM information_schema.TABLES WHERE table_schema = 'TPM_Scheduler' AND table_name = 'qrtz_blob_triggers' limit 1;") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.MySql)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
                break;
            case DbProvider.Sqlite:
                exist = con.ExecuteScalar<int>("SELECT 1 FROM sqlite_master WHERE type = 'table' and name='QRTZ_BLOB_TRIGGERS';") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.SQLite)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
                break;
            case DbProvider.PostgreSQL:
                exist = con.ExecuteScalar<int>("SELECT 1 FROM pg_namespace WHERE nspname = 'qrtz_triggers' LIMIT 1;") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.PostgreSQL)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
                break;
        }

    }


}
