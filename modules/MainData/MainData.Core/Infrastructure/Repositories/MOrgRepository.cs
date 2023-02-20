
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MOrgRepository : RepositoryAbstract<MOrgEntity>, IMOrgRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MOrgEntity>> Query(MOrgQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 根据工作站ID获取组织层级
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    public async Task<(MOrgEntity headOffice, MOrgEntity division, MOrgEntity market, MOrgEntity big, MOrgEntity office, MOrgEntity station)> GetOrgLevelByStationID(Guid stationId)
    {
        MOrgEntity headOffice = null;
        MOrgEntity division = null;
        MOrgEntity market = null;
        MOrgEntity big = null;
        MOrgEntity office = null;
        var station = await Find(f => f.Id == stationId && f.Type == (int)OrgEnumType.Station).ToFirst();
        if (station != null)
        {
            office = await Find(f => f.Id == station.ParentId).ToFirst();
        }
        if (office != null)
        {
            big = await Find(f => f.Id == office.ParentId).ToFirst();
        }
        if (big != null)
        {
            market = await Find(f => f.Id == big.ParentId).ToFirst();
        }
        if (market != null)
        {
            division = await Find(f => f.Id == big.ParentId).ToFirst();
        }
        if (division != null)
        {
            headOffice = await Find(f => f.Id == big.ParentId).ToFirst();
        }
        return (headOffice, division, market, big, office, station);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private Data.Abstractions.Queryable.IQueryable<MOrgEntity> QueryBuilder(MOrgQueryDto dto)
    {
        var query = Find();
        return query;
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<int> Delete(IEnumerable<Guid> ids)
    {
        int result = 0;
        foreach (var id in ids)
        {
            if (await Delete(id)) result++;
        }
        return result;
    }
}