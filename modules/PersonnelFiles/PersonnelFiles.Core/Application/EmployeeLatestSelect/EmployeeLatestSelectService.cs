using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using CRB.TPM.Utils.Map;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect;

public class EmployeeLatestSelectService : IEmployeeLatestSelectService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeLatestSelectRepository _repository;

    public EmployeeLatestSelectService(IMapper mapper, IEmployeeLatestSelectRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> Query(Guid employeeId, EmployeeLatestSelectQueryDto dto)
    {
        return await _repository.Query(employeeId, dto);
    }

    public async Task<PagingQueryResultModel<EmployeeLatestSelectVo>> QueryView(Guid employeeId, EmployeeLatestSelectQueryDto dto)
    {
        return await _repository.QueryView(employeeId, dto);
    }

    public async Task<EmployeeLatestSelectEntity> Get(Guid employeeId, Guid relationId, IUnitOfWork uow)
    {
        return await _repository.Get(employeeId, relationId, uow);
    }
}
