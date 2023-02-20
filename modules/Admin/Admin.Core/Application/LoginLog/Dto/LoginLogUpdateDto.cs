using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.LoginLog.Dto;


[ObjectMap(typeof(LoginLogEntity), true)]
public class LoginLogUpdateDto : LoginLogAddDto
{

    [Required(ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}
