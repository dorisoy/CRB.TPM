using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Vo
{
    [ObjectMap(typeof(MDistributorEntity), true)]
    public class MDistributorEditVo
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string DistributorCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string DistributorName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string DistributorType { get; set; }
        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }
        /// <summary>
        /// 大区id
        /// </summary>
        public Guid DutyregionId { get; set; }
        /// <summary>
        /// 业务部id
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }
        /// <summary>
        /// 法人主体id
        /// </summary>
        public Guid EntityId { get; set; }
        /// <summary>
        /// CRM编码，现在等于客户编码(DistributorCode)
        /// </summary>
        public string CrmCode { get; set; }
        /// <summary>
        /// 1表示主户；2管理开户的子户；3TPM虚拟子户
        /// </summary>
        public int DetailType { get; set; }
        /// <summary>
        /// 经销商编码 用于经销商分析。主户填其自身编码；虚拟子户、管理开户的填
        /// 主客户编码。
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// 子户父级经销商Id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 子户父级经销商编码
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 是否跟CRM变动工作站
        /// </summary>
        public int IsSynchronizeCrmStation { get; set; }
    }
}
