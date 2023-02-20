using System;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Sharding;

namespace CRB.TPM.Data.Core.Sharding.Providers
{
    /// <summary>
    /// 季度分表策略提供器
    /// </summary>
    internal class QuarterShardingPolicyProvider : IShardingPolicyProvider
    {
        /// <summary>
        /// 解析表名
        /// </summary>
        /// <param name="descriptor">实体信息描述符</param>
        /// <param name="dateTime">日期</param>
        /// <param name="next">是否解析下一张表</param>
        /// <returns>返回：{表名}_{yyyy}{季度} 格式</returns>
        public string ResolveTableName(IEntityDescriptor descriptor, DateTime dateTime, bool next = false)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var quarter = 1;

            if (month >= 1 && month < 4)
                quarter = 1;
            else if (month >= 4 && month < 7)
                quarter = 2;
            else if (month >= 7 && month < 10)
                quarter = 3;
            else if (month >= 10)
                quarter = 4;

            if (next)
            {
                if (quarter == 4)
                {
                    year++;
                    quarter = 1;
                }
                else
                {
                    quarter++;
                }
            }

            return $"{descriptor.TableName}_{year}{quarter}";
        }
    }
}
