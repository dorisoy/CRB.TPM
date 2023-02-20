using System.Threading.Tasks;

namespace CRB.TPM.Utils.App;

/// <summary>
/// 应用关闭处理接口
/// </summary>
public interface IAppShutdownHandler
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <returns></returns>
    Task Handle();
}