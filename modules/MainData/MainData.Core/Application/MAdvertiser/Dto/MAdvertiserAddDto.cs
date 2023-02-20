using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;

/// <summary>
/// 广告商添加模型
/// </summary>
[ObjectMap(typeof(MAdvertiserEntity))]
public class MAdvertiserAddDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string AdvertiserCode { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string AdvertiserName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string RegionCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ADJC { get; set; }

}