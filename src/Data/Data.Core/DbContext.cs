using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.Schema;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using Microsoft.Extensions.Options;
using CRB.TPM.Data.Sharding;

namespace CRB.TPM.Data.Core;


/// <summary>
/// 数据库上下文（仓储模式）
/// </summary>
public abstract class DbContext : IDbContext
{
    #region ==属性==

    /// <summary>
    /// 数据库配置项
    /// </summary>
    public DbOptions Options { get; internal set; }

    /// <summary>
    /// 日志记录器
    /// </summary>
    public DTPger Logger { get; internal set; }

    /// <summary>
    /// 数据库适配器
    /// </summary>
    public IDbAdapter Adapter { get; internal set; }

    /// <summary>
    /// 数据库架构提供器
    /// </summary>
    public ISchemaProvider SchemaProvider { get; internal set; }

    /// <summary>
    /// 代码生成提供器
    /// </summary>
    public ICodeFirstProvider CodeFirstProvider { get; internal set; }

    /// <summary>
    /// 账户信息解析器
    /// </summary>
    public IOperatorResolver AccountResolver { get; internal set; }

    /// <summary>
    /// 实体描述符列表
    /// </summary>
    public IList<IEntityDescriptor> EntityDescriptors { get; } = new List<IEntityDescriptor>();

    /// <summary>
    /// 仓储描述符列表
    /// </summary>
    public IList<IRepositoryDescriptor> RepositoryDescriptors { get; } = new List<IRepositoryDescriptor>();

    /// <summary>
    /// 连接字符串管理器
    /// </summary>
    public IReadWriteConnectionStringManager ConnectionStringManager { get; internal set; }

    #endregion

    #region ==方法==

    public IDbConnection NewConnection(NodeType? nodeType = null)
    {
        //如果启用读写分离，则从配置节点的主库创建提供器实例
        //可以在modules 加载时解析所有ReadWriteSeparationOptions，加入到连接管理器，然后通过 Options.ModuleCode 获取对应模块的链接

        if (!Options.UseReadWriteSeparation)
            return Adapter.NewConnection(Options.ConnectionString);

        //Node node = null;

        nodeType = nodeType == null ? NodeType.Master : nodeType;

        //根据策略获取节点连接字符串
        var connString = ConnectionStringManager.GetConnectionString(nodeType.Value);

        //switch (nodeType)
        //{
        //    case NodeType.Master:
        //        node = Options?.ReadWriteSeparationOptions?.Master?.FirstOrDefault();
        //        break;
        //    case NodeType.Slave:
        //        node = Options?.ReadWriteSeparationOptions?.Slave?.FirstOrDefault();
        //        break;
        //}
        //// Check.NotNull(node?.ConnectionString, $"{this.GetType().Name}-Exception:节点 {node?.Name} 的数据库连接未配置！");
        //return Adapter.NewConnection(string.IsNullOrWhiteSpace(node?.ConnectionString) ? Options.ConnectionString : node?.ConnectionString);

        return Adapter.NewConnection(string.IsNullOrWhiteSpace(connString) ? Options.ConnectionString : connString);
    }

    public IDbConnection NewConnection(string connectionString, NodeType? nodeType = null)
    {
        return Adapter.NewConnection(connectionString, nodeType);
    }

    public IUnitOfWork NewUnitOfWork(IsolationLevel? isolationLevel = null)
    {
        return new UnitOfWork(this, isolationLevel);
    }

    #endregion
}




/// <summary>
///  数据库上下文（客户端单例模式）
/// </summary>
public class ClientDbContext : IClientDbContext
{
    public DbOptions Options { get; internal set; }
    public static IClient Client { get; internal set; }
    public IDatabase Db { get; internal set; }

    /// <summary>
    /// 初始化配置
    /// </summary>
    public void Init()
    {
        var opt = Options;
        switch (opt.Provider)
        {
            case DbProvider.MySql:
                {
                    var config = ConnectionStringBuilder.ParsingMySql(opt.ConnectionString);
                    Client = ShardingFactory.CreateClient(DataBaseType.MySql, config, opt.ConnectionString);
                }
                break;
            case DbProvider.SqlServer:
                {
                    var config = ConnectionStringBuilder.ParsingSqlServer(opt.ConnectionString);
                    Client = ShardingFactory.CreateClient(DataBaseType.SqlServer2012, config, opt.ConnectionString);
                }
                break;
            case DbProvider.PostgreSQL:
                {
                    var config = ConnectionStringBuilder.ParsingNpgsql(opt.ConnectionString);
                    Client = ShardingFactory.CreateClient(DataBaseType.Postgresql, new(), opt.ConnectionString);
                }
                break;
            case DbProvider.Sqlite:
                {
                    var config = ConnectionStringBuilder.ParsingSqlite(opt.ConnectionString);
                    Client = ShardingFactory.CreateClient(DataBaseType.Sqlite, new(), opt.ConnectionString);
                }
                break;
            case DbProvider.Oracle:
                {
                    var config = ConnectionStringBuilder.ParsingOracle(opt.ConnectionString);
                    Client = ShardingFactory.CreateClient(DataBaseType.Oracle, new(), opt.ConnectionString);
                }
                break;
        }

        //自动创建表
        Client.AutoCreateTable = true;
        //以当前模块创建数据库
        Db = Client.GetDatabase(opt.ModuleCode);
    }
}