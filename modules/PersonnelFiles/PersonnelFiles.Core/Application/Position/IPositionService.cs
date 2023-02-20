using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Position.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Position
{
    public interface IPositionService
    {
        Task<IResultModel> Add(PositionAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<IResultModel> Edit(Guid id);
        Task<IResultModel> Get(Guid id);
        Task<PagingQueryResultModel<PositionEntity>> Query(PositionQueryDto dto);
        Task<IResultModel> Update(PositionUpdateDto dto);
    }
}