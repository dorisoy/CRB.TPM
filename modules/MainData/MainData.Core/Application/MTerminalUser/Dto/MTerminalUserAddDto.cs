using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;

/// <summary>/// 终端与经销商的关系信息添加模型
/// </summary>
[ObjectMap(typeof(MTerminalUserEntity))]
public class MTerminalUserAddDto
{
    /// <summary>
    /// 终端id
    /// </summary>
    [Required()]
    public Guid TerminalId { get; set; }

    /// <summary>
    /// 业务员
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

}