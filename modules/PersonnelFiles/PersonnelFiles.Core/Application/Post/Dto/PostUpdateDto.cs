using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.PS.Core.Application.Post.Dto;

[ObjectMap(typeof(PostEntity), true)]
public class PostUpdateDto : PostAddDto
{
    [Required(ErrorMessage = "请选择要修改的岗位")]
    //[Range(1, int.MaxValue, ErrorMessage = "请选择数据")]
    public Guid Id { get; set; }
}