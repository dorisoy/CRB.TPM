
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

/// <summary>
/// 管理开户关系
/// </summary>
public class MDistributorManageAccountDto 
{
    /// <summary>
    /// 主户
    /// </summary>
    public string DistributorMainAccount { get; set; }
    /// <summary>
    /// 子户
    /// </summary>
    public string DistributorSubAccount { get; set; }

    /// <summary>
    /// 经销商id
    /// </summary>
    public Guid DistributorMainAccountId { get; set; }
}