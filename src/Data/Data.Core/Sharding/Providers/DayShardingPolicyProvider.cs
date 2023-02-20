using System;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Sharding;

namespace CRB.TPM.Data.Core.Sharding.Providers
{
    /// <summary>
    /// 按天分表提供器
    /// </summary>
    internal class DayShardingPolicyProvider : IShardingPolicyProvider
    {
        /// <summary>
        /// 解析表名
        /// </summary>
        /// <param name="descriptor">实体信息描述符</param>
        /// <param name="dateTime">日期</param>
        /// <param name="next">是否解析下一张表</param>
        /// <returns>返回：{表名}_{yyyyMMdd} 格式</returns>
        public string ResolveTableName(IEntityDescriptor descriptor, DateTime dateTime, bool next = false)
        {
            if (next)
            {
                //递增天
                dateTime = dateTime.AddDays(1);
            }

            return $"{descriptor.TableName}_{dateTime:yyyyMMdd}";
        }
    }
}
