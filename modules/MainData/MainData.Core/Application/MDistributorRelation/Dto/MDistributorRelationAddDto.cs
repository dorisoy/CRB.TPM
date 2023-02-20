using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;

/// <summary>
/// 经销商分销商关系表添加模型
/// </summary>
[ObjectMap(typeof(MDistributorRelationEntity))]
public class MDistributorRelationAddDto
{
    /// <summary>
    /// 经销商id
    /// </summary>
    [Required]
    public Guid DistributorId1 { get; set; }

    /// <summary>
    /// 分销商id
    /// </summary>
    [Required]
    public Guid DistributorId2 { get; set; }
}