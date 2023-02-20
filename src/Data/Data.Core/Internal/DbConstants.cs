using System;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Data.Core.Internal;

/// <summary>
/// 数据库相关常量
/// </summary>
internal static class DbConstants
{
    /// <summary>
    /// 租户列名称
    /// </summary>
    public const string TENANT_COLUMN_NAME = "TenantId";

    /// <summary>
    /// 
    /// </summary>
    public static readonly Type ENTITY_INTERFACE_TYPE = typeof(IEntity);

}