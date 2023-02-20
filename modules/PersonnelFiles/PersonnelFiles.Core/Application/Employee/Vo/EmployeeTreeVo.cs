using CRB.TPM.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Employee.Vo;

/// <summary>
/// 员工树返回结果
/// </summary>
public class EmployeeTreeVo
{
    /// <summary>
    /// 源编号
    /// </summary>
    public string SourceId { get; set; }

    /// <summary>
    /// 类型 0：部门 1：人员
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 人员岗位名称
    /// </summary>
    public string PostName { get; set; }

    /// <summary>
    /// 人员性别
    /// </summary>
    public Sex Sex { get; set; }

    /// <summary>
    /// 人员部门路径
    /// </summary>
    public string DepartmentPath { get; set; }
}
