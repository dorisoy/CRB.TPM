using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using System.Collections;
using System.Collections.Generic;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

/// <summary>
/// 经销商/分销商查询模型
/// </summary>
public class MDistributorQueryDto : GlobalOrgFilterDto
{
    //private IList<Guid> headOfficeIds;
    //private IList<Guid> divisionIds;
    //private IList<Guid> marketingIds;
    //private IList<Guid> dutyregionIds;
    //private IList<Guid> departmentIds;
    //private IList<Guid> stationIds;
    //private IList<Guid> distributorIds;

    /// <summary>
    /// 客户编码/名称
    /// </summary>
    public string Name { get; set; }
    ///// <summary>
    ///// 客户类型
    ///// </summary>
    //public int DistributorType { get; set; }
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

    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<MEntityExportDto> ExportModel { get; set; } = new ();

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