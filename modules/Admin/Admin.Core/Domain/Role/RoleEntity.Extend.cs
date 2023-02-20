using CRB.TPM.Data.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Domain.Role;

public partial class RoleEntity
{
    /// <summary>
    /// 菜单组名称
    /// </summary>
    [NotMappingColumn]
    public string MenuGroupName { get; set; }
}