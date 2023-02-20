using System;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Core.Extensions;
using CRB.TPM.Data.Abstractions.Entities;
using Microsoft.Extensions.Options;

namespace CRB.TPM.Data.Core;

public abstract class CodeFirstProviderAbstract : ICodeFirstProvider
{
    protected readonly CodeFirstOptions Options;
    protected readonly IDbContext Context;
    protected readonly IServiceCollection Service;
    public CodeFirstOptions GetOptions() => this.Options;

    protected CodeFirstProviderAbstract(CodeFirstOptions options, IDbContext context, IServiceCollection service)
    {
        Options = options;
        Context = context;
        Service = service;
    }

    public abstract void CreateDatabase();
    public abstract void CreateTable(IEntity entity = null);
    public abstract void CreateNextTable();



    /// <summary>
    /// 解析分表的下一个表名
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="next"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected string ResolveTableName(IEntityDescriptor descriptor, bool next = false, IEntity entity = null)
    {
        var tableName = descriptor.TableName;
        if (descriptor.IsSharding && Options.EnableShardingPolicy)
        {
            //分表字段
            var date = descriptor.GetShardingFieldValue(entity);

            //根据指定日期解析表名称
            tableName = descriptor.ShardingPolicyProvider.ResolveTableName(descriptor, date, next);
        }

        return tableName;
    }

  
    
}