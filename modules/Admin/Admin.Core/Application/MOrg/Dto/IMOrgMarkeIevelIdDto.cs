using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto
{
    public interface IMOrgMarkeIevelIdDto
    {
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
    }

    public interface IMOrgIevelIdsDto
    {
        /// <summary>
        /// 雪花ids
        /// </summary>
        public IList<Guid> HeadOfficeIds { get; set; }
        /// <summary>
        /// 事业部ids
        /// </summary>
        public IList<Guid> DivisionIds { get; set; }
        /// <summary>
        /// 营销中心ids
        /// </summary>
        public IList<Guid> MarketingIds { get; set; }
        /// <summary>
        /// 大区ids
        /// </summary>
        public IList<Guid> DutyregionIds { get; set; }
        /// <summary>
        /// 业务部ids
        /// </summary>
        public IList<Guid> DepartmentIds { get; set; }
        /// <summary>
        /// 工作站ids
        /// </summary>
        public IList<Guid> StationIds { get; set; }
        /// <summary>
        /// 客户ids
        /// </summary>
        public IList<Guid> DistributorIds { get; set; }
    }
    public interface IMOrgIevelIdsDtoExt : IMOrgIevelIdsDto
    {
        IMOrgIevelIdsDto SetIds(OrgEnumType orgEnumType, IList<Guid> ids);
    }

    public class IOrgIevelIdsDtoFactory
    {
        public static IMOrgIevelIdsDtoExt CreateInstance()
        {
            return new DefaultMOrgIevelIdsDto();
        }
    }

}
