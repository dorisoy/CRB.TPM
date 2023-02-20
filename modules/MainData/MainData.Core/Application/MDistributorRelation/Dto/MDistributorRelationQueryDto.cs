using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using System;
using System.Collections.Generic;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;

/// <summary>
/// 经销商分销商关系表查询模型
/// </summary>
public class MDistributorRelationQueryDto : GlobalOrgFilterDto
{
    //private IList<Guid> headOfficeIds;
    //private IList<Guid> divisionIds;
    //private IList<Guid> marketingIds;
    //private IList<Guid> dutyregionIds;
    //private IList<Guid> departmentIds;
    //private IList<Guid> stationIds;
    //private IList<Guid> distributorIds;

    ///// <summary>
    ///// 雪花ids
    ///// </summary>
    //public IList<Guid> HeadOfficeIds { get => headOfficeIds.RemoveGuidEmpty(); set => headOfficeIds = value; }
    ///// <summary>
    ///// 事业部ids
    ///// </summary>
    //public IList<Guid> DivisionIds { get => divisionIds.RemoveGuidEmpty(); set => divisionIds = value; }
    ///// <summary>
    ///// 营销中心ids
    ///// </summary>
    //public IList<Guid> MarketingIds { get => marketingIds.RemoveGuidEmpty(); set => marketingIds = value; }
    ///// <summary>
    ///// 大区ids
    ///// </summary>
    //public IList<Guid> DutyregionIds { get => dutyregionIds.RemoveGuidEmpty(); set => dutyregionIds = value; }
    ///// <summary>
    ///// 业务部ids
    ///// </summary>
    //public IList<Guid> DepartmentIds { get => departmentIds.RemoveGuidEmpty(); set => departmentIds = value; }
    ///// <summary>
    ///// 工作站ids
    ///// </summary>
    //public IList<Guid> StationIds { get => stationIds.RemoveGuidEmpty(); set => stationIds = value; }
    ///// <summary>
    ///// 经销商编码
    ///// </summary>
    //public IList<Guid> DistributorIds { get => distributorIds.RemoveGuidEmpty(); set => distributorIds = value; }

    /// <summary>
    /// 分销商编码
    /// </summary>
    public Guid DistributorId2 { get; set; }

    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<MDistributorRelationEntity> ExportModel { get; set; } = new ();

    /// <summary>
    /// 导出数量
    /// </summary>
    public long ExportCount { get; set; }

    /// <summary>
    /// 导出数量限制
    /// </summary>
    public virtual int ExportCountLimit => 50000;

    /// <summary>
    /// 是否超出导出数量限制
    /// </summary>
    public bool IsOutOfExportCountLimit => ExportCount > ExportCountLimit;


    /// <summary>
    /// 是否是导出操作
    /// </summary>
    public bool IsExport => ExportModel != null;

    /// <summary>
    /// 查询数量
    /// </summary>
    public bool QueryCount { get; set; } = true;

    #endregion
}