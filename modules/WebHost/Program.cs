
using CRB.TPM.Host.Web;

namespace WebHost;

public partial class Program
{
    public static void Main(string[] args)
    {
        new HostBootstrap().Run(args);
    }
}