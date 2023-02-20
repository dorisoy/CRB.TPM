
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributor
{
    /// <summary>
    /// 经销商/分销商
    /// </summary>
    [Table("M_Distributor")]
    public partial class MDistributorEntity : EntityBaseSoftDelete<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Length(10)]
        public string DistributorCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [Length(60)]
        public string DistributorName { get; set; } = string.Empty;

        /// <summary>
        /// 类型
        /// </summary>
        public int DistributorType { get; set; }

        /// <summary>
        /// 业务实体id
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// CRM编码
        /// </summary>
        [Length(10)]
        public string CrmCode { get; set; } = string.Empty;

        /// <summary>
        /// 1表示主户；2管理开户的子户；3TPM虚拟子户
        /// </summary>
        public int DetailType { get; set; }

        /// <summary>
        /// 经销商编码 用于经销商分析。主户填其自身编码；虚拟子户、管理开户的填主客户编码。
        /// </summary>
        [Length(30)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 子户父亲
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 是否跟CRM变动工作站
        /// </summary>
        //[Length(1)]
        public int IsSynchronizeCRMStation { get; set; } //= string.Empty;
    }
}
