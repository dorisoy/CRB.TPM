
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.MOrg
{
    /// <summary>
    /// 组织表仓储
    /// </summary>
    public interface IMOrgRepository : IRepository<MOrgEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MOrgEntity>> Query(MOrgQueryDto dto);
        /// <summary>
        /// 根据工作站获取组织层级
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        Task<(MOrgEntity headOffice, MOrgEntity division, MOrgEntity market, MOrgEntity big, MOrgEntity office, MOrgEntity station)> GetOrgLevelByStationID(Guid stationId);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<Guid> ids);

    }
}


