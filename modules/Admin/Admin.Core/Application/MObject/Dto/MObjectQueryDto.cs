using CRB.TPM.Data.Abstractions.Query;
using System;

namespace CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;

/// <summary>
/// 对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键查询模型
/// </summary>
public class MObjectQueryDto : QueryDto
{
        /// <summary>
        /// 营销中心id、大区id、业务部id、工作站id、客户id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 层级（1-雪花总部、2-事业部、3-营销中心、4-大区、5-业务部、6-工作站、7-客户）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 对象编码
        /// </summary>
        public string ObjectCode { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 营销中心编码
        /// </summary>
        public string MarketingCode { get; set; }

        /// <summary>
        /// 营销中心名称
        /// </summary>
        public string MarketingName { get; set; }

        /// <summary>
        /// 大区id
        /// </summary>
        public Guid BigAreaId { get; set; }

        /// <summary>
        /// 大区编码
        /// </summary>
        public string BigAreaCode { get; set; }

        /// <summary>
        /// 大区名称
        /// </summary>
        public string BigAreaName { get; set; }

        /// <summary>
        /// 业务部id
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// 业务部编码
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// 业务部名称
        /// </summary>
        public string OfficeName { get; set; }

        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }

        /// <summary>
        /// 工作站编码
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// 工作站名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        public Guid DistributorId { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string DistributorCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string DistributorName { get; set; }

}