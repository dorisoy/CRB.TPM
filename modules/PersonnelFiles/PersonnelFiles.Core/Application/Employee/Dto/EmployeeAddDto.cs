using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Enums;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.PS.Core.Application.Employee.Dto;


/// <summary>
/// 员工基本信息添加模型
/// </summary>
[ObjectMap(typeof(EmployeeEntity))]
public class EmployeeAddDto
{
    /// <summary>
    /// 所属部门
    /// </summary>
    [GuidNotEmptyValidation(ErrorMessage = "请选择部门")]
    public int DepartmentId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "请输入姓名")]
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Sex Sex { get; set; }

    /// <summary>
    /// 性质
    /// </summary>
    public EmployeeNature Nature { get; set; }

    /// <summary>
    /// 照片(附件编号)
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// 岗位编号
    /// </summary>
    public int? PostId { get; set; }

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


    #region ==账户相关属性==

    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "请输入用户名")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<Guid> Roles { get; set; }

    #endregion
}

