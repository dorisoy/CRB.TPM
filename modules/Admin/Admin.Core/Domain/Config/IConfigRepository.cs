using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Config.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Domain.Config
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public interface IConfigRepository : IRepository<ConfigEntity>
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ConfigEntity> Get(ConfigType type, string code);
    }
}
