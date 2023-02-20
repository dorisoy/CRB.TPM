using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;

/// <summary>
/// 营销中心配置查询模型
/// </summary>
public class MMarketingSetupQueryDto : GlobalOrgFilterDto
{
    /// <summary>
    /// 筛选编码/名字
    /// </summary>
    public string Name { get; set; }
}