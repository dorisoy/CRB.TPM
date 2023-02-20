using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.MOrg;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;

namespace CRB.TPM.Mod.MainData.Core.Application.MOrg;

public class MOrgService : MOrgServiceAbstract
{
    private readonly MainDataDbContext _mainDataDbContext;

    public MOrgService(IMapper mapper,
    IMOrgRepository mOrgRepository,
    ICacheProvider cacheHandler,
    IServiceProvider serviceProvider,
    IAccountResolver accountResolver,
    AdminCacheKeys cacheKeys,
    IMObjectRepository mObjectRepository, 
    MainDataDbContext mainDataDbContext) : base(mapper, cacheHandler, serviceProvider, mOrgRepository, accountResolver, cacheKeys, mObjectRepository)
    {

        _mainDataDbContext = mainDataDbContext;
 
    }

    //TODO... 在这里扩展
}
