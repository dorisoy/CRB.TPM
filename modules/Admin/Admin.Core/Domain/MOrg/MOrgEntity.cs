
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Admin.Core.Domain.MOrg
{
    /// <summary>
    /// 组织表
    /// </summary>
    [Table("M_Org")]
    public partial class MOrgEntity : EntityBaseSoftDelete<Guid>
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        [Length(10)]
        public string OrgCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
		[Nullable]
        [Length(60)]
        public string OrgName { get; set; } = string.Empty;

        /// <summary>
        /// 层级（10-雪花总部、20-事业部、30-营销中心、40-大区、50-业务部、60-工作站）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 作废映射，多个用“|”分隔
        /// </summary>
		[Nullable]
        [Length(500)]
        public string InvalidMapping { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
		[Nullable]
        [Length(500)]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int Enabled { get; set; }
        /// <summary>
        /// 组织类型 1业务部门 2职能部门
        /// </summary>
        public int Attribute { get; set; }
    }
}
