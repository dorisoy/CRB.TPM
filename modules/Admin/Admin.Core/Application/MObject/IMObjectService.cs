using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MObject;

/// <summary>
/// 对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键服务
/// </summary>
public interface IMObjectService
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<MObjectEntity>> Query(MObjectQueryDto dto);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(MObjectAddDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">编号</param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Update(MObjectUpdateDto dto);

    /// <summary>
    /// 创建数据同步批量处理临时表，并写入临时数据
    /// </summary>
    /// <param name="dtos">临时数据</param>
    /// <returns></returns>
    Task<string> CreateSyncTmpTable(List<MObjectSyncDto> dtos);

    /// <summary>
    /// 批量处理（存在则更新，不存在则新增）
    /// </summary>
    /// <param name="tmpMObjectTableName">临时表</param>
    /// <param name="uow">工作单元</param>
    /// <returns></returns>
    Task<IResultModel> BatchProcess(string tmpMObjectTableName, IUnitOfWork uow = null);

    /// <summary>
    /// 根据层级获取政策对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<IList<MObjectEntity>> QueryMObjectByType(OrgEnumType type);

    /// <summary>
    /// 根据对象编码获取政策对象
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<MObjectEntity> GetMObjectByCode(string code);
}
