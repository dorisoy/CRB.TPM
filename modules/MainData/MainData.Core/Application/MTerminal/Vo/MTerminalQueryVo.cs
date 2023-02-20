using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using System;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Vo
{
    public class MTerminalQueryVo : IMOrgMarkeIevelVo
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 终端编码
        /// </summary>
        public string TerminalCode { get; set; }
        /// <summary>
        /// 终端名称
        /// </summary>
        public string TerminalName { get; set; }
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
        /// <summary>
        /// 业务线
        /// </summary>
        public string SaleLine { get; set; }
        /// <summary>
        /// 一级类型
        /// </summary>
        public string Lvl1Type { get; set; }
        /// <summary>
        /// 二级类型
        /// </summary>
        public string Lvl2Type { get; set; }
        /// <summary>
        /// 三级类型
        /// </summary>
        public string Lvl3Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
