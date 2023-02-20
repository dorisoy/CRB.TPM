using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Department.Vo;

/// <summary>
/// 组织架构部门树
/// </summary>

[ObjectMap(typeof(DepartmentEntity), true)]
public class DepartmentTreeVo
{
    /// <summary>
    /// 编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 唯一编码
    /// </summary>
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
