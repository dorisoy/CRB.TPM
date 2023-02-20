
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Admin.Core.Domain.MObject
{
    /// <summary>
    /// 对象表，营销中心、大区、业务部、工作站、客户 的主键是 数据本身的主键
    /// </summary>
    [Table("M_Object")]
    public partial class MObjectEntity : Entity<Guid>
    {
        /// <summary>
        /// 层级（10-雪花总部、20-事业部、30-营销中心、40-大区、50-业务部、60-工作站、70-客户）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 对象编码
        /// </summary>
        [Length(10)]
        public string ObjectCode { get; set; } = string.Empty;

        /// <summary>
        /// 对象名称
        /// </summary>
        [Length(60)]
        public string ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// 总部Id
        /// </summary>
        public Guid HeadOfficeId { get; set; }

        /// <summary>
        /// 总部编码
        /// </summary>
        [Length(10)]
        public string HeadOfficeCode { get; set; } = string.Empty;

        /// <summary>
        /// 总部名称
        /// </summary>
        [Length(60)]
        public string HeadOfficeName { get; set; } = string.Empty;

        /// <summary>
        /// 事业部/区域Id
        /// </summary>
        public Guid DivisionId { get; set; }

        /// <summary>
        /// 事业部/区域编码
        /// </summary>
        [Length(10)]
        public string DivisionCode { get; set; } = string.Empty;

        /// <summary>
        /// 事业部/区域名称
        /// </summary>
        [Length(60)]
        public string DivisionName { get; set; } = string.Empty;

        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 营销中心编码
        /// </summary>
        [Length(10)]
        public string MarketingCode { get; set; } = string.Empty;

        /// <summary>
        /// 营销中心名称
        /// </summary>
        [Length(60)]
        public string MarketingName { get; set; } = string.Empty;

        /// <summary>
        /// 大区id
        /// </summary>
        public Guid BigAreaId { get; set; }

        /// <summary>
        /// 大区编码
        /// </summary>
        [Length(10)]
        public string BigAreaCode { get; set; } = string.Empty;

        /// <summary>
        /// 大区名称
        /// </summary>
        [Length(60)]
        public string BigAreaName { get; set; } = string.Empty;

        /// <summary>
        /// 业务部id
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// 业务部编码
        /// </summary>
        [Length(10)]
        public string OfficeCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务部名称
        /// </summary>
        [Length(60)]
        public string OfficeName { get; set; } = string.Empty;

        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }

        /// <summary>
        /// 工作站编码
        /// </summary>
        [Length(10)]
        public string StationCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作站名称
        /// </summary>
        [Length(60)]
        public string StationName { get; set; } = string.Empty;

        /// <summary>
        /// 客户id
        /// </summary>
        public Guid DistributorId { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        [Length(10)]
        public string DistributorCode { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称
        /// </summary>
        [Length(100)]
        public string DistributorName { get; set; } = string.Empty;
        /// <summary>
        /// 是否有效
        /// </summary>
        public int Enabled { get; set; }
    }
}
