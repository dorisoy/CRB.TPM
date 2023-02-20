using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.AuditInfo;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.AuditInfo.Dto;


[ObjectMap(typeof(AuditInfoEntity), true)]
public class AuditInfoUpdateDto : AuditInfoAddDto
{

    [Required(ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}
