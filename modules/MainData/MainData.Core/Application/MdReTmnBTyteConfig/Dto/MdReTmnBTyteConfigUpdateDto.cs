
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;

namespace CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;

/// <summary>
///  终端业态关系表 M_Re_Tmn_BTyte_Config更新模型
/// </summary>
[ObjectMap(typeof(MdReTmnBTyteConfigEntity), true)]
public class MdReTmnBTyteConfigUpdateDto : MdReTmnBTyteConfigAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择 终端业态关系表 M_Re_Tmn_BTyte_Config")]
    [Required(ErrorMessage = "请选择要修改的 终端业态关系表 M_Re_Tmn_BTyte_Config")]
    public Guid Id { get; set; }
}