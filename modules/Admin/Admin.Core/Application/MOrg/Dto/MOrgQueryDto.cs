using CRB.TPM.Data.Abstractions.Query;
using System;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

/// <summary>
/// 组织表查询模型
/// </summary>
public class MOrgQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 层级（1-雪花总部、2-事业部、3-营销中心、4-大区、5-业务部、6-工作站）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 作废映射，多个用“|”分隔
        /// </summary>
        public string InvalidMapping { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Deleted { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid? DeletedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Deleter { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

}