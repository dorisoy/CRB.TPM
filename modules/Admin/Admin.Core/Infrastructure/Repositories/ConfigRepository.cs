using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Config;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class ConfigRepository : RepositoryAbstract<ConfigEntity>, IConfigRepository
{
    public Task<ConfigEntity> Get(ConfigType type, string code)
    {
        return Get(m => m.Type == type && m.Code == code);
    }
}
