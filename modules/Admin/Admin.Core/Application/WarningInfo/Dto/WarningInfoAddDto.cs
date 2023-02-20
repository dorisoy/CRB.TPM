using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;

/// <summary>
/// 账户新增模型
/// </summary>
[ObjectMap(typeof(WarningInfoEntity))]
public class WarningInfoAddDto
{
    public WarningInfoType Type { get; set; }

    public string OrgId { get; set; }

    public string OrgCodeName { get; set; }

    [Nullable]
    public string Mess1 { get; set; }

    [Nullable]
    public string Mess2 { get; set; }

    [Nullable]
    public string Mess3 { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}