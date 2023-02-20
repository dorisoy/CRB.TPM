using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Utils.Enums;

namespace CRB.TPM.Mod.PS.Core.Domain.Employee;

/// <summary>
/// 员工基本信息
/// </summary>
[Table("Employee")]
public partial class EmployeeEntity : EntityBaseSoftDelete<Guid>
{
    /// <summary>
    /// 关联账户编号
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属部门
    /// </summary>
    public Guid DepartmentId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性质
    /// </summary>
    public EmployeeNature Nature { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Sex Sex { get; set; }

    /// <summary>
    /// 照片(附件编号)
    /// </summary>
    [Nullable]
    [Length(200)]
    public string Picture { get; set; }

    /// <summary>
    /// 岗位编号
    /// </summary>
    public Guid? PostId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public EmployeeStatus Status { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime JoinDate { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// LDAP账号
    /// </summary>
    public string LDAP { get; set; }
}
