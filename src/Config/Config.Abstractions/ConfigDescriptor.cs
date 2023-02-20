using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Abstractions
{

    /// <summary>
    /// 配置类型
    /// </summary>
    public enum ConfigType
    {
        /// <summary>
        /// 库配置
        /// </summary>
        [Description("库配置")]
        Library,
        /// <summary>
        /// 模块配置
        /// </summary>
        [Description("模块配置")]
        Module
    }

    /// <summary>
    /// 配置描述符
    /// </summary>
    public class ConfigDescriptor
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ConfigType Type { get; set; }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementType { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 变更事件列表
        /// </summary>
        public List<Type> ChangeEvents { get; set; } = new List<Type>();

        /// <summary>
        /// 实例
        /// </summary>
        [JsonIgnore]
        public IConfig Instance { get; set; }

    }
}
