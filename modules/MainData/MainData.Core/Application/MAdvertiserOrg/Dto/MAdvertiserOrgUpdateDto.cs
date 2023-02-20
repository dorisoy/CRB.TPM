
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;

/// <summary>
/// 广告商营销组织关系表 M_Re_Org_AD更新模型
/// </summary>
[ObjectMap(typeof(MAdvertiserOrgEntity), true)]
public class MAdvertiserOrgUpdateDto : MAdvertiserOrgAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择广告商营销组织关系表 M_Re_Org_AD")]
    [Required(ErrorMessage = "请选择要修改的广告商营销组织关系表 M_Re_Org_AD")]
    public Guid Id { get; set; }
}