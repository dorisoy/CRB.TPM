using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Utils.Validations;
using System;

namespace CRB.TPM.Mod.Admin.Core.Application.Menu.Dto;

public class MenuQueryDto : QueryDto
{
    /// <summary>
    /// 分组编号
    /// </summary>
    [GuidNotEmptyValidation(ErrorMessage = "分组编号不能为空")]
    public Guid GroupId { get; set; }

    /// <summary>
    /// 父节点
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string Name { get; set; }
}
