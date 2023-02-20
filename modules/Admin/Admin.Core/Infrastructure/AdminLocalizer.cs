using Microsoft.Extensions.Localization;
using CRB.TPM.Module.Core;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure
{
    /// <summary>
    /// Admin 多语言
    /// </summary>
    [SingletonInject(true)]
    public class AdminLocalizer : ModuleLocalizerAbstract
    {
        public AdminLocalizer(IStringLocalizer<AdminLocalizer> localizer) : base(localizer)
        {
        }
    }
}
