using System;
using CRB.TPM.Data.Abstractions.Descriptors;

namespace CRB.TPM.Data.Abstractions.Sharding
{
    /// <summary>
    /// 用于表示根据时间进行分表策略的提供器
    /// </summary>
    public interface IShardingPolicyProvider
    {
        /// <summary>
        /// 根据指定日期解析表名称
        /// </summary>
        /// <param name="descriptor">实体描述符</param>
        /// <param name="dateTime">时间</param>
        /// <param name="next">是否解析下一张表</param>
        /// <returns></returns>
        string ResolveTableName(IEntityDescriptor descriptor, DateTime dateTime, bool next = false);
    }
}
