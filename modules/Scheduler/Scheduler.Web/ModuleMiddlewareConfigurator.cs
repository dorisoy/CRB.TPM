using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Module.Web;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CRB.TPM.Mod.Scheduler.Web;

/// <summary>
/// 配置中间件
/// </summary>
public class ModuleMiddlewareConfigurator : IModuleMiddlewareConfigurator
{
    /// <summary>
    /// 配置中间件,开机启动
    /// </summary>
    /// <param name="app"></param>
    public void Configure(IApplicationBuilder app)
    {
        var quartzServer = app.ApplicationServices.GetService<IQuartzServer>();
        //使用当前模块的数据库配置信息来配置Quartz的数据库配置
        //var dbOptions = app.ApplicationServices.GetService<DbOptions>();
        //var configProvider = app.ApplicationServices.GetService<IConfigProvider>();
        //自定义系统配置
        //var quartzConfig = configProvider.Get<QuartzConfig>();
        //quartzConfig.Provider = SqlDialect2QuartzProvider(dbOptions.Provider);
        //quartzConfig.ConnectionString = dbOptions.ConnectionString;
        //configProvider.Set(JsonConvert.SerializeObject(quartzConfig));
        if (quartzServer != null)
            quartzServer.Start();
    }

    private QuartzProvider SqlDialect2QuartzProvider(DbProvider sqlDialect)
    {
        switch (sqlDialect)
        {
            case DbProvider.MySql: return QuartzProvider.MySql;
            case DbProvider.Sqlite: return QuartzProvider.SQLite;
            case DbProvider.PostgreSQL: return QuartzProvider.PostgreSQL;
            case DbProvider.Oracle: return QuartzProvider.Oracle;
            default: return QuartzProvider.SqlServer;
        }
    }
}
