using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;

/// <summary>
/// 终端与人员的关系同步临时表模型
/// </summary>
public class MTerminalUserSyncDto
{
    /// <summary>
    ///  
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 终端编码
    /// </summary>
    public string TerminalCode { get; set; }

    /// <summary>
    /// 业务员
    /// </summary>
    public string UserBP { get; set; }

    /// <summary>
    /// 终端id
    /// </summary>
    public Guid? TerminalId { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public Guid? AccountId { get; set; }

    /// <summary>
    /// 是否删除（同步CRM数据时使用） 是D的为删除
    /// </summary>
    public string UpdateMode { get; set; } = string.Empty;

}