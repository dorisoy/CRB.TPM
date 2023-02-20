using CRB.TPM.Mod.Admin.Core.Application.MObject;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.MainData.Core.Infrastructure;
using CRB.TPM.Utils.Map;

namespace CRB.TPM.Mod.MainData.Core.Application.MObject;

public class MObjectService : MObjectServiceAbstract
{
    private readonly MainDataDbContext  _mainDataDbContext;

    public MObjectService(IMapper mapper,
        AdminDbContext dbContext,
        MainDataDbContext mainDataDbContext,
        IMObjectRepository repository) : base(mapper, dbContext, repository)
    {
        _mainDataDbContext = mainDataDbContext;
    }


    //TODO... 在这里扩展

}
