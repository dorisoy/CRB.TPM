using CRB.TPM.Utils.Annotations;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;


[ObjectMap(typeof(LoginLogEntity), true)]
public class LoginLogUpdateDto : LoginLogAddDto
{

    [Required(ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}
