using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;
using CRB.TPM.Utils.Map;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using System;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo;

public class EmployeeLeaveInfoSevice : IEmployeeLeaveInfoSevice
{
    private readonly IMapper _mapper;
    private readonly IEmployeeLeaveInfoRepository _repository;
    private readonly IAccountRepository _accountRepository;

    public EmployeeLeaveInfoSevice(IMapper mapper,
         IAccountRepository accountRepository,
        IEmployeeLeaveInfoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
        _accountRepository = accountRepository;
    }

    public Task<PagingQueryResultModel<EmployeeLeaveInfoEntity>> Query(EmployeeLeaveInfoQueryDto dto)
    {
        return _repository.Query(dto);
    }

    public Task<EmployeeLeaveInfoEntity> GetByEmployeeId(Guid employeeId)
    {
        return _repository.GetByEmployeeId(employeeId);
    }
}
