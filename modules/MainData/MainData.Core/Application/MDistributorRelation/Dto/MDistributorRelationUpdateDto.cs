
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;

/// <summary>
/// 经销商分销商关系表更新模型
/// </summary>
[ObjectMap(typeof(MDistributorRelationEntity), true)]
public class MDistributorRelationUpdateDto : MDistributorRelationAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择经销商分销商关系表")]
    [Required(ErrorMessage = "请选择要修改的经销商分销商关系表")]
    public Guid Id { get; set; }
}