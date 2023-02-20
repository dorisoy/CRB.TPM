using CRB.TPM.Data.Abstractions.Query;
using System;

namespace CRB.TPM.Mod.Admin.Core.Application.DictItem.Dto;

public class DictItemQueryDto : QueryDto
{
    /// <summary>
    /// 分组编码
    /// </summary>
    public string GroupCode { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    public string DictCode { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }
}