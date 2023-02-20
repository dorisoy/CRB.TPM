
using CRB.TPM.Host.Web;

namespace WebHost;

/// <summary>
/// 以Module模块为例，这里测试最小化的模块请求生命周期，你将会得到一个测试API文档
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        new HostBootstrap().Run(args);
    }
}