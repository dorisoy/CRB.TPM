using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Config.Abstractions;


namespace CRB.TPM.Mod.Admin.Core.Domain.Config
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Table("SYS_Config")]
    public partial class ConfigEntity : Entity<Guid>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ConfigType Type { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Nullable]
        [Length(4000)]
        public string Value { get; set; }
    }
}
