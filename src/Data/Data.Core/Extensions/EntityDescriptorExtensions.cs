using System;
using System.Collections.Generic;
using System.Linq;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Entities;
using static Dapper.SqlMapper;

namespace CRB.TPM.Data.Core.Extensions;

internal static class EntityDescriptorExtensions
{
    /// <summary>
    /// 获取已删除字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetDeletedColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsSoftDelete)
            return GetColumnNameByPropertyName(descriptor, "Deleted");

        return string.Empty;
    }

    /// <summary>
    /// 获取删除时间属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetDeletedTimeColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsSoftDelete)
            return GetColumnNameByPropertyName(descriptor, "DeletedTime");

        return string.Empty;
    }

    /// <summary>
    /// 获取删除人属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetDeletedByColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsSoftDelete)
            return GetColumnNameByPropertyName(descriptor, "DeletedBy");

        return string.Empty;
    }

    /// <summary>
    /// 获取删除人名称属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetDeleterColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsSoftDelete)
            return GetColumnNameByPropertyName(descriptor, "Deleter");

        return string.Empty;
    }

    /// <summary>
    /// 获取修改人属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetModifiedByColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsEntityBase)
            return GetColumnNameByPropertyName(descriptor, "ModifiedBy");

        return string.Empty;
    }

    /// <summary>
    /// 获取修改人属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetModifierColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsEntityBase)
            return GetColumnNameByPropertyName(descriptor, "Modifier");

        return string.Empty;
    }

    /// <summary>
    /// 获取修改时间属性对应字段名称
    /// </summary>
    /// <returns></returns>
    public static string GetModifiedTimeColumnName(this IEntityDescriptor descriptor)
    {
        if (descriptor.IsEntityBase)
            return GetColumnNameByPropertyName(descriptor, "ModifiedTime");

        return string.Empty;
    }

    private static string GetColumnNameByPropertyName(this IEntityDescriptor descriptor, string propertyName)
    {
        return descriptor.Columns.First(m => m.PropertyInfo.Name.Equals(propertyName)).Name;
    }


    /// <summary>
    /// 获取实体的时间分表字段
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static DateTime GetShardingFieldValue(this IEntityDescriptor descriptor, IEntity entity)
    {
        DateTime defaultValue = DateTime.Now;

        //默认系统时间
        if (entity == null)
            return defaultValue;

        try
        {
            //foreach (var p in descriptor.Columns.Select(s => s.PropertyInfo))
            //{
            //    var attr = p.GetCustomAttributes(typeof(ShardingFieldAttribute), true).Select(s => s as ShardingFieldAttribute).FirstOrDefault();

            //    //启用时间分表字段特性
            //    if ((attr != null) && attr.Enable)
            //    {
            //        //取实例时间分表字段属性值
            //        var pro = p.GetValue(entity);
            //        if (pro != null)
            //        {
            //            defaultValue = (DateTime)p.GetValue(entity);
            //        }
            //        break;
            //    }
            //}

            List<object> values;
            //检查分表字段必须为非空时间类型
            bool valid = entity.Validate(out values);
            return valid ? (DateTime)values.First() : defaultValue;
        }
        catch (Exception)
        {
            return defaultValue;
        }

    }
}