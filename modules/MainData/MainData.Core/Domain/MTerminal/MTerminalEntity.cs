
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminal
{
    /// <summary>
    /// 终端信息
    /// </summary>
    [Table("M_Terminal")]
    public partial class MTerminalEntity : Entity<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Length(10)]
        public string TerminalCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [Length(40)]
        public string TerminalName { get; set; } = string.Empty;

        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }

        /// <summary>
        /// 业务线
        /// </summary>
		[Nullable]
        [Length(10)]
        public string SaleLine { get; set; } = string.Empty;

        /// <summary>
        /// 一级类型
        /// </summary>
		[Nullable]
        [Length(10)]
        public string Lvl1Type { get; set; } = string.Empty;

        /// <summary>
        /// 二级类型
        /// </summary>
		[Nullable]
        [Length(10)]
        public string Lvl2Type { get; set; } = string.Empty;

        /// <summary>
        /// 三级类型
        /// </summary>
		[Nullable]
        [Length(10)]
        public string Lvl3Type { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
		[Nullable]
        [Length(500)]
        public string Address { get; set; } = string.Empty;

    }
}
