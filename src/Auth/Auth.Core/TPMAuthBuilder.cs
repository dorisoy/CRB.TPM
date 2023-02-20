using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRB.TPM.Auth.Core;

public class CRBTPMAuthBuilder
{
    public IServiceCollection Services { get; set; }

    /// <summary>
    /// 配置属性
    /// </summary>
    public IConfiguration Configuration { get; set; }

    public CRBTPMAuthBuilder(IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }
}