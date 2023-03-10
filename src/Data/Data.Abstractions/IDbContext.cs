using System.Collections.Generic;
using System.Data;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.Schema;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
//Sharding
using CRB.TPM.Data.Sharding;
using NetTopologySuite.IO;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Data.Abstractions;

/// <summary>
/// 数据库上下文（仓储模式）
/// </summary>
public interface IDbContext
{
    #region ==属性==

    /// <summary>
    /// 数据库配置项
    /// </summary>
    DbOptions Options { get; }

    /// <summary>
    /// 日志记录器
    /// </summary>
    DTPger Logger { get; }

    /// <summary>
    /// 数据库适配器
    /// </summary>
    IDbAdapter Adapter { get; }

    /// <summary>
    /// 数据库架构提供器
    /// </summary>
    ISchemaProvider SchemaProvider { get; }

    /// <summary>
    /// 代码生成提供器
    /// </summary>
    ICodeFirstProvider CodeFirstProvider { get; }

    /// <summary>
    /// 账户信息解析器
    /// </summary>
    IOperatorResolver AccountResolver { get; }

    /// <summary>
    /// 实体描述符列表
    /// </summary>
    IList<IEntityDescriptor> EntityDescriptors { get; }

    /// <summary>
    /// 仓储描述符列表
    /// </summary>
    IList<IRepositoryDescriptor> RepositoryDescriptors { get; }

    /// <summary>
    /// 连接字符串管理器
    /// </summary>
    IReadWriteConnectionStringManager ConnectionStringManager { get; }

    #endregion

    #region ==方法==

    /// <summary>
    /// 创建新的连接
    /// </summary>
    IDbConnection NewConnection(NodeType? nodeType = null);

    /// <summary>
    /// 使用指定字符串创建连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="nodeType">节点类型</param>
    /// <returns></returns>
    IDbConnection NewConnection(string connectionString, NodeType? nodeType = null);

    /// <summary>
    /// 创建工作单元
    /// </summary>
    /// <param name="isolationLevel">指定锁级别</param>
    /// <returns></returns>
    IUnitOfWork NewUnitOfWork(IsolationLevel? isolationLevel = null);


    #endregion
}


/// <summary>
/// 数据库上下文（客户端单例模式）
/// </summary>
public interface IClientDbContext 
{
    void Init();
    /// <summary>
    /// 数据库配置项
    /// </summary>
    DbOptions Options { get; }
    static IClient Client { get; }
    IDatabase Db { get; }

    //static ITable<IEntity> Table { get; }
}