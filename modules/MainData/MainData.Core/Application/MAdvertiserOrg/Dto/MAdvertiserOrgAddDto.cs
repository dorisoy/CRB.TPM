using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;

/// <summary>
/// 广告商营销组织关系表 M_Re_Org_AD添加模型
/// </summary>
[ObjectMap(typeof(MAdvertiserOrgEntity))]

public class MAdvertiserOrgAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string MarketOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string BigAreaOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string OfficeOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string StationOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public Guid AdvertiserId { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int Status { get; set; }

}