
using CRB.TPM.Host.Web;

namespace WebHost;

/// <summary>
/// ��Moduleģ��Ϊ�������������С����ģ�������������ڣ��㽫��õ�һ������API�ĵ�
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        new HostBootstrap().Run(args);
    }
}