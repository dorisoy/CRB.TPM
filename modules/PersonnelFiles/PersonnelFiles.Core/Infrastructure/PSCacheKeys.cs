using System;
using System.ComponentModel;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.PS.Core.Infrastructure;

/// <summary>
/// PS管理模块缓存键
/// </summary>
[SingletonInject]
public class PSCacheKeys
{
    /// <summary>
    /// 部门树
    /// </summary>
    public string DepartmentTree() => $"PS:DEPARTMENT_TREE";

    /// <summary>
    /// 岗位下拉列表
    /// </summary>
    public string PostSelect() => $"PS:POST_SELECT";

    /// <summary>
    /// 人员基本信息
    /// </summary>
    public string EmployeeBaseInfo(Guid id) => $"PS:EMPLOYEE:BASE_INFO:{id}";

    /// <summary>
    /// 人员树
    /// </summary>
    public string EmployeeTree() => $"PS:EMPLOYEE:TREE";

}