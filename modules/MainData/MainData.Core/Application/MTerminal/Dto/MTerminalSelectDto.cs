using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto
{
    public class MTerminalSelectDto : GlobalOrgFilterDto
    {
        //private IList<Guid> marketingIds;
        //private IList<Guid> dutyregionIds;
        //private IList<Guid> departmentIds;
        //private IList<Guid> stationIds;
        //private IList<Guid> headOfficeIds;
        //private IList<Guid> divisionIds;
        //private IList<Guid> distributorIds;

        /// <summary>
        /// ids数组，如果传了ids那么返回这些ids的信息就行 忽略分页
        /// </summary>
        public IList<Guid> Ids { get; set; }
        /// <summary>
        /// 筛选
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// 雪花ids
        ///// </summary>
        //public IList<Guid> HeadOfficeIds { get => headOfficeIds.RemoveGuidEmpty(); set => headOfficeIds = value; }
        ///// <summary>
        ///// 事业部ids
        ///// </summary>
        //public IList<Guid> DivisionIds { get => divisionIds.RemoveGuidEmpty(); set => divisionIds = value; }
        ///// <summary>
        ///// 营销中心id
        ///// </summary>
        //public IList<Guid> MarketingIds { get => marketingIds.RemoveGuidEmpty(); set => marketingIds = value; }
        ///// <summary>
        ///// 大区id
        ///// </summary>
        //public IList<Guid> DutyregionIds { get => dutyregionIds.RemoveGuidEmpty(); set => dutyregionIds = value; }
        ///// <summary>
        ///// 业务部id
        ///// </summary>
        //public IList<Guid> DepartmentIds { get => departmentIds.RemoveGuidEmpty(); set => departmentIds = value; }
        ///// <summary>
        ///// 工作站id
        ///// </summary>
        //public IList<Guid> StationIds { get => stationIds.RemoveGuidEmpty(); set => stationIds = value; }
        ///// <summary>
        ///// 经销商ids
        ///// </summary>
        //public IList<Guid> DistributorIds { get => distributorIds.RemoveGuidEmpty(); set => distributorIds = value; }
    }
}
