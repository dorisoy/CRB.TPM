
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;

namespace CRB.TPM.Mod.MainData.Core.Application.MdSaleLine.Dto;

/// <summary>
/// 业务线 D_SaleLine更新模型
/// </summary>
[ObjectMap(typeof(MdSaleLineEntity), true)]
public class MdSaleLineUpdateDto : MdSaleLineAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择业务线 D_SaleLine")]
    [Required(ErrorMessage = "请选择要修改的业务线 D_SaleLine")]
    public Guid Id { get; set; }
}