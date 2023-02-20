using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;

/// <summary>
/// 终端与经销商的关系信息添加模型
/// </summary>
[ObjectMap(typeof(MTerminalDistributorEntity))]
public class MTerminalDistributorAddDto
{
    /// <summary>
    /// 终端编码
    /// </summary>
    [Required]
    public Guid TerminalId { get; set; }

    /// <summary>
    /// 经销商编码
    /// </summary>
    [Required]
    public Guid DistributorId { get; set; }

}