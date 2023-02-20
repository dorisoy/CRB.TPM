using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Vo;

/// <summary>
/// 人员离职模型
/// </summary>
public class EmployeeLeaveVo
{
    [Required(ErrorMessage = "请选择人员")]
    //[Range(1, int.MaxValue, ErrorMessage = "请选择人员")]
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// 离职类型
    /// </summary>
    [Required(ErrorMessage = "请选择离职类型")]
    public string Type { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [Required(ErrorMessage = "请选择离职日期")]
    public DateTime ApplyDate { get; set; }

    /// <summary>
    /// 离岗日期
    /// </summary>
    [Required(ErrorMessage = "请选择离岗日期")]
    public DateTime LeaveDate { get; set; }
}
