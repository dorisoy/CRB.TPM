using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.PS.Core.Domain.Department;

using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.PS.Core.Application.Department.Dto;

[ObjectMap(typeof(DepartmentEntity))]
public class DepartmentAddDto
{
    /// <summary>
    /// 父级ID
    /// </summary>
    [Required(ErrorMessage = "{0} 必须填写")]
    //[Range(0, 10000, ErrorMessage = "超出范围")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "部门名称不能为空")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "{0}输入长度不正确")]
    public string Name { get; set; }

    /// <summary>
    /// 唯一编码
    /// </summary>
    [Required(ErrorMessage = "唯一编码不能为空")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "{0}输入长度不正确")]
    public string Code { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 完整路径
    /// </summary>
    public string FullPath { get; set; }
}