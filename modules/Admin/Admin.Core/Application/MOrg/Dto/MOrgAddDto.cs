using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

/// <summary>
/// 组织表添加模型
/// </summary>
[ObjectMap(typeof(MOrgEntity))]
public class MOrgAddDto
{
    /// <summary>
    /// 组织编码
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "组织编码最长30位")]
    public string OrgCode { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "组织名称最长60位")]
    public string OrgName { get; set; }

    /// <summary>
    /// 层级（1-雪花总部、2-事业部、3-营销中心、4-大区、5-业务部、6-工作站）
    /// </summary>
    [Required]
    public int Type { get; set; }

    /// <summary>
    /// 父级id
    /// </summary>
    [Required]
    public Guid ParentId { get; set; }

    /// <summary>
    /// 作废映射，多个用“|”分隔
    /// </summary>
    [MaxLength(500, ErrorMessage = "作废映射最长500位")]
    public string InvalidMapping { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(500, ErrorMessage = "备注最长500位")]
    public string Remark { get; set; }

    /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool Enabled { get; set; }
    /// <summary>
    /// 组织属性 1 业务部门 2 职能部门
    /// </summary>
    public int Attribute { get; set; }
}