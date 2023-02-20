using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.WarningInfo
{
    public interface IWarningInfoService
    {
        Task<PagingQueryResultModel<WarningInfoEntity>> Query(WarningInfoQueryDto dto);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResultModel> BulkAdd(List<WarningInfoAddDto> dtos);
    }
}