using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;

/// <summary>/// 终端信息添加模型
/// </summary>
[ObjectMap(typeof(MTerminalEntity))]
public class MTerminalAddDto
{
    /// <summary>
    /// 终端编码
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "终端编码最长10位")]
    public string TerminalCode { get; set; }
    /// <summary>
    /// 终端名称
    /// </summary>
    [Required]
    [MaxLength(40, ErrorMessage = "终端名称最长40位")]
    public string TerminalName { get; set; }
    /// <summary>
    /// 工作站id
    /// </summary>
    [Required]
    public Guid StationId { get; set; }
    /// <summary>
    /// 业务线
    /// </summary>
    [MaxLength(10, ErrorMessage = "业务线最长10位")]
    public string SaleLine { get; set; }
    /// <summary>
    /// 业务线
    /// </summary>
    public string Lvl1Type { get; set; }
    /// <summary>
    /// 业务线
    /// </summary>
    public string Lvl2Type { get; set; }
    /// <summary>
    /// 业务线
    /// </summary>
    public string Lvl3Type { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// 地址
    /// </summary>
    [MaxLength(500, ErrorMessage = "地址最长500位")]
    public string Address { get; set; }
}