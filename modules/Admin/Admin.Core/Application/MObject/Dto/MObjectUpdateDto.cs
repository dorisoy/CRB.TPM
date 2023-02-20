
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;

/// <summary>
/// 对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键更新模型
/// </summary>
[ObjectMap(typeof(MObjectEntity), true)]
public class MObjectUpdateDto : MObjectAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键")]
    [Required(ErrorMessage = "请选择要修改的对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键")]
    public Guid Id { get; set; }
}