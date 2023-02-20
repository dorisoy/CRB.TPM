using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;
using System;
using System.Collections.Generic;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;

/// <summary>
/// 终端与经销商的关系信息查询模型
/// </summary>
public class MTerminalDistributorQueryDto : GlobalOrgFilterDto
{
    //private IList<Guid> headOfficeIds;
    //private IList<Guid> divisionIds;
    //private IList<Guid> marketingIds;
    //private IList<Guid> dutyregionIds;
    //private IList<Guid> departmentIds;
    //private IList<Guid> stationIds;
    //private IList<Guid> distributorIds;

    /// <summary>
    /// 终端id,使用终端下拉独立组件
    /// </summary>
    public Guid TerminalId { get; set; }
    /// <summary>
    /// 编号/名称
    /// </summary>
    public string Name { get; set; }

    ///// <summary>
    ///// 客户id
    ///// </summary>
    //public IList<Guid> DistributorIds { get => distributorIds.RemoveGuidEmpty(); set => distributorIds = value; }
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

    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<MTerminalDistributorEntity> ExportModel { get; set; } = new ();

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