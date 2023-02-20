using CRB.TPM.Mod.Scheduler.Core.Domain.Group;
using CRB.TPM.Utils.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;


/// <summary>
/// 任务组添加模型
/// </summary>
[ObjectMap(typeof(GroupEntity))]
public class GroupAddDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "请输入组名称")]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "请输入编码")]
    public string Code { get; set; }
}