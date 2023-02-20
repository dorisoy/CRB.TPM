using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Cache.Abstractions;

namespace CRB.TPM.Cache.Core
{
    internal class CacheBuilder : ICacheBuilder
    {
        public CacheBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public void Build()
        {
            //暂时没什么可以构造的
        }
    }
}
