using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Abstractions
{
    /// <summary>
    /// 配置提供器
    /// </summary>
    public enum ConfigProvider
    {
        /// <summary>
        /// 内存存储
        /// </summary>
        [Description("Memory")]
        Memory,

        /// <summary>
        /// 数据存储
        /// </summary>
        [Description("DB")]
        DB,

        /// <summary>
        /// Redis 存储
        /// </summary>
        [Description("Redis")]
        Redis,
    }
}
