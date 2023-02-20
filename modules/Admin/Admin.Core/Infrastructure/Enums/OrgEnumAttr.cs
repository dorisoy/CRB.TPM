using System.ComponentModel;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Enums
{
    public enum OrgEnumAttr : int
    {
        [Description("业务部门")]
        ZSORG = 1,
        [Description("职能部门")]
        ZDIVISION = 2,
    }
}
