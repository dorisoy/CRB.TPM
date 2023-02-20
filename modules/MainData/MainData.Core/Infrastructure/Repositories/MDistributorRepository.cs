
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MDistributorRepository : RepositoryAbstract<MDistributorEntity>, IMDistributorRepository
{
    private IQueryable<MDistributorEntity, MEntityEntity, MObjectEntity, MDistributorEntity> queryBuilder;

    ///// <summary>
    ///// 根据组织权限生成Sql条件字符串
    ///// </summary>
    //public BuildSqlWhereStrByOrgAuthFunc BuildSqlWhereStrAuthFunc { get; set; }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<MDistributorQueryVo>> QueryPage(MDistributorQueryDto dto)
    {
        await this.QueryBuild().QueryWhereBuild(dto);
        return await queryBuilder.ToPaginationResultVo<MDistributorQueryVo>(dto.Paging);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IList<MDistributorQueryVo>> Query(MDistributorQueryDto dto)
    {
        await this.QueryBuild().QueryWhereBuild(dto);

        var rows = await queryBuilder.ToList<MDistributorQueryVo>();
        return rows;
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

    /// <summary>
    /// 根据id获取经销商和分销商
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    public async Task<(MDistributorEntity mdb1, MDistributorEntity mdb2)> GetByDistributorId(Guid id1, Guid id2)
    {
        int distributorType1 = (int)DistributorTypeEnum.Distributor1;
        int distributorType2 = (int)DistributorTypeEnum.Distributor2;
        var resByDistributorId1 = await Find(f => f.Id == id1 && f.DistributorType == distributorType1).ToFirst();
        var resByDistributorId2 = await Find(f => f.Id == id2 && f.DistributorType == distributorType2).ToFirst();
        return (resByDistributorId1, resByDistributorId2);
    }

    /// <summary>
    /// Select下拉接口
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PagingQueryResultModel<DistributorSelectVo>> Select(DistributorSelectDto dto)
    {
        if (dto.Type != (int)DistributorTypeEnum.Distributor1 && dto.Type != (int)DistributorTypeEnum.Distributor2)
        {
            return new PagingQueryResultModel<DistributorSelectVo>().Success();
        }
        var query = Find();
        query = query.WhereNotNull(dto.Name, w => w.DistributorName.Contains(dto.Name)).Where(w => w.DistributorType == dto.Type);
        if (dto.Ids.NotNullAndEmpty())
        {
            dto.Page.Size = int.MaxValue;
            var res = await query
                        .Where(w => dto.Ids.Contains(w.Id))
                        .Select(s => new { Label = s.DistributorName, Value = s.Id }).ToPaginationResultVo<DistributorSelectVo>(dto.Paging);
        }

        var filter = await dto.BuildFilter(SP, OrgEnumType.Distributor, "T2");

        //var queryPage = query
        //    .LeftJoin<MObjectEntity>(j => j.T1.StationId == j.T2.StationId)
        //    .WhereNotNull(BuildSqlWhereStrAuthFunc, BuildSqlWhereStrAuthFunc(null, OrgEnumType.Distributor, "T2").Result)
        //    .OrderByDescending(o => o.T1.Id);

        var queryPage = query
            .LeftJoin<MObjectEntity>(j => j.T1.StationId == j.T2.StationId)
            .Where(filter)
            .OrderByDescending(o => o.T1.Id);

        return await query.Select(s => new { Label = s.DistributorName, Value = s.Id }).ToPaginationResultVo<DistributorSelectVo>(dto.Paging);
    }

    /// <summary>
    /// 是否可以变更过客户编码
    /// </summary>
    /// <returns>true=可以更新，false=不能更新</returns>
    public async Task<(bool res, string msg)> IsChangeDistributorCode(string code)
    {
        //经销分销绑定，就不能修改编码
        string sql = @$"SELECT COUNT(1) FROM M_DistributorRelation WHERE DistributorCode1='{code}' OR DistributorCode2='{code}'";
        var res = await base.QueryFirstOrDefault<long>(sql);
        return (res == 0, "经销分销关系已绑定");
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <returns></returns>
    private MDistributorRepository QueryBuild()
    {
        queryBuilder = Find()
                        .LeftJoin<MEntityEntity>(l => l.T1.EntityId == l.T2.Id)
                        .LeftJoin<MObjectEntity>(j => j.T1.Id == j.T3.DistributorId)
                        .LeftJoin<MDistributorEntity>(parent => parent.T1.ParentId == parent.T4.Id)
                        .Select(s => new
                        {
                            Id = s.T1.Id,
                            DistributorCode = s.T1.DistributorCode,
                            DistributorName = s.T1.DistributorName,
                            DistributorType = s.T1.DistributorType,
                            StationId = s.T3.StationId,
                            StationName = s.T3.StationName,
                            DepartmentId = s.T3.OfficeId,
                            DepartmentName = s.T3.OfficeName,
                            DutyregionId = s.T3.BigAreaId,
                            DutyregionName = s.T3.BigAreaName,
                            MarketingId = s.T3.MarketingId,
                            MarketingName = s.T3.MarketingName,
                            EntityId = s.T2.Id,
                            EntityName = s.T2.EntityName,
                            CrmCode = s.T1.CrmCode,
                            DetailType = s.T1.DetailType,
                            CustomerCode = s.T1.CustomerCode,
                            ParentId = s.T1.ParentId,
                            ParentCode = s.T4.DistributorCode,
                            IsSynchronizeCrmStation = s.T1.IsSynchronizeCRMStation,
                            Creator = s.T1.Creator,
                            CreatedTime = s.T1.CreatedTime,
                            Modifier = s.T1.Modifier,
                            ModifiedTime = s.T1.ModifiedTime,
                        });
        return this;
    }

    /// <summary>
    /// 构建条件查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private async Task<MDistributorRepository> QueryWhereBuild(MDistributorQueryDto dto)
    {
        queryBuilder = queryBuilder
            .WhereIf(dto.Name.NotNull(), w => w.T1.DistributorCode.Contains(dto.Name) || w.T1.DistributorName.Contains(dto.Name))
            .WhereIf(dto.DistributorType > 0, w => w.T1.DistributorType == dto.DistributorType);

        if (queryBuilder != null)
        {

            var filter = await dto.BuildFilter(SP, OrgEnumType.Distributor, "T3");

            //queryBuilder = queryBuilder
            //.Where(BuildSqlWhereStrAuthFunc(dto, OrgEnumType.Distributor, "T3").Result);

            queryBuilder = queryBuilder.Where(filter);
        }
        else
        {
            queryBuilder = queryBuilder
            .WhereIf(dto.MarketingIds.NotNullAndEmpty(), w => dto.MarketingIds.Contains(w.T3.MarketingId))
            .WhereIf(dto.DutyregionIds.NotNullAndEmpty(), w => dto.DutyregionIds.Contains(w.T3.BigAreaId))
            .WhereIf(dto.DepartmentIds.NotNullAndEmpty(), w => dto.DepartmentIds.Contains(w.T3.OfficeId))
            .WhereIf(dto.StationIds.NotNullAndEmpty(), w => dto.StationIds.Contains(w.T3.StationId));
        }
        return this;
    }


 
}