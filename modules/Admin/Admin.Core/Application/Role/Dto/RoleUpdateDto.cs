using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.Role.Dto;

/// <summary>
/// 角色更新
/// </summary>
[ObjectMap(typeof(RoleEntity), true)]
public class RoleUpdateDto : RoleAddDto
{
    [Required(ErrorMessage = "请选择要修改的角色")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的角色")]
    public Guid Id { get; set; }

    /// <summary>
    /// 锁定的，不允许修改
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// CRMCode
    /// </summary>
    public string CRMCode { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public string OrgId { get; set; }
    public string OrgName { get; set; }
}