using Microsoft.Extensions.Localization;
using CRB.TPM.Module.Core;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure
{
    /// <summary>
    /// 这里是创建的是MainData集成模块 多语言
    /// </summary>
    [SingletonInject(true)]
    public class MainDataLocalizer : ModuleLocalizerAbstract
    {
        public MainDataLocalizer(IStringLocalizer<MainDataLocalizer> localizer) : base(localizer)
        {
        }
    }
}
