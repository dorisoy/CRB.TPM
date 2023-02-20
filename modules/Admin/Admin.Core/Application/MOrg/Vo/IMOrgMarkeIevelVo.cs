using System;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo
{
    public interface IMOrgMarkeIevelVo
    {
        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }
        /// <summary>
        /// 营销中心名称
        /// </summary>
        public string MarketingName { get; set; }
        /// <summary>
        /// 大区id
        /// </summary>
        public Guid DutyregionId { get; set; }
        /// <summary>
        /// 大区名称
        /// </summary>
        public string DutyregionName { get; set; }
        /// <summary>
        /// 业务部id
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 业务部名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 工作站id
        /// </summary>
        public Guid StationId { get; set; }
        /// <summary>
        /// 工作站名称
        /// </summary>
        public string StationName { get; set; }
    }
}
