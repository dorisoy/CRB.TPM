using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Utils.Annotations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;

/// <summary>
/// 组织架构部门树
/// </summary>

[ObjectMap(typeof(MOrgEntity), true)]
public class MOrgTreeVo : MOrgVo
{
    /// <summary>
    /// 作废映射，多个用“|”分隔
    /// </summary>
    public string InvalidMapping { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 完整路径
    /// </summary>
    public string FullPath { get; set; }

}

/// <summary>
/// 表示组织架构
/// </summary>
public class MOrgBaseVo
{
    /// <summary>
    /// 编号
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 组织编码
    /// </summary>
    public string OrgCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}



/// <summary>
/// 表示组织架构
/// </summary>
public class MOrgVo: MOrgBaseVo
{
    /// <summary>
    /// 父级ID
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 层级（1-雪花总部、2-事业部、3-营销中心、4-大区、5-业务部、6-工作站）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 组织属性，用于表示职能部门还是业务部门
    /// </summary>
    public int Attribute { get; set; }

    /// <summary>
    /// 当前节点路径
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<MOrgVo> Paths { get; set; } 

}

/// <summary>
/// 表示组织架构层级
/// </summary>
public class MOrgLevelVo
{
    /// <summary>
    /// 雪花
    /// </summary>
    public List<MOrgVo> HeadOffice { get; set; } = new();

    /// <summary>
    /// 事业部
    /// </summary>
    public List<MOrgVo> Dbs { get; set; } = new();

    /// <summary>
    /// 营销中心
    /// </summary>
    public List<MOrgVo> MarketingCenters { get; set; } = new();

    /// <summary>
    /// 大区
    /// </summary>
    public List<MOrgVo> SaleRegions { get; set; } = new();

    /// <summary>
    /// 业务部
    /// </summary>
    public List<MOrgVo> Departments { get; set; } = new();

    /// <summary>
    /// 工作站
    /// </summary>
    public List<MOrgVo> Stations { get; set; } = new();
}


/// <summary>
/// 表示组织架构层级
/// </summary>
public class MOrgLevelTreeVo
{
    /// <summary>
    /// 当前节点虽在层级
    /// </summary>
    public OrgEnumType Level { get; set; }

    /// <summary>
    /// 雪花
    /// </summary>
    public MOrgBaseVo HeadOfficeID { get; set; } = new();

    /// <summary>
    /// 事业部
    /// </summary>
    public MOrgBaseVo DbID { get; set; } = new();

    /// <summary>
    /// 营销中心
    /// </summary>
    public MOrgBaseVo MarketingCenterID { get; set; } = new();

    /// <summary>
    /// 大区
    /// </summary>
    public MOrgBaseVo SaleRegionID { get; set; } = new();

    /// <summary>
    /// 业务部
    /// </summary>
    public MOrgBaseVo DepartmentID { get; set; } = new();

    /// <summary>
    /// 工作站
    /// </summary>
    public MOrgBaseVo StationID { get; set; } = new();
}
