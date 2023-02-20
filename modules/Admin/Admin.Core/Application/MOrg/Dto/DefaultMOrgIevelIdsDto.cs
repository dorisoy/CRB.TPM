using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MObject;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto
{
    public class DefaultMOrgIevelIdsDto : IMOrgIevelIdsDtoExt
    {
        private IList<Guid> headOfficeIds;
        private IList<Guid> divisionIds;
        private IList<Guid> marketingIds;
        private IList<Guid> dutyregionIds;
        private IList<Guid> departmentIds;
        private IList<Guid> stationIds;
        private IList<Guid> distributorIds;

        /// <summary>
        /// 雪花ids
        /// </summary>
        public IList<Guid> HeadOfficeIds { get => headOfficeIds.RemoveGuidEmpty(); set => headOfficeIds = value; }
        /// <summary>
        /// 事业部ids
        /// </summary>
        public IList<Guid> DivisionIds { get => divisionIds.RemoveGuidEmpty(); set => divisionIds = value; }
        /// <summary>
        /// 营销中心ids
        /// </summary>
        public IList<Guid> MarketingIds { get => marketingIds.RemoveGuidEmpty(); set => marketingIds = value; }
        /// <summary>
        /// 大区ids
        /// </summary>
        public IList<Guid> DutyregionIds { get => dutyregionIds.RemoveGuidEmpty(); set => dutyregionIds = value; }
        /// <summary>
        /// 业务部ids
        /// </summary>
        public IList<Guid> DepartmentIds { get => departmentIds.RemoveGuidEmpty(); set => departmentIds = value; }
        /// <summary>
        /// 工作站ids
        /// </summary>
        public IList<Guid> StationIds { get => stationIds.RemoveGuidEmpty(); set => stationIds = value; }
        /// <summary>
        /// 经销商编码ids
        /// </summary>
        public IList<Guid> DistributorIds { get => distributorIds.RemoveGuidEmpty(); set => distributorIds = value; }

        Dictionary<OrgEnumType, string> map = new Dictionary<OrgEnumType, string>()
            {
                { OrgEnumType.HeadOffice, nameof(IMOrgIevelIdsDto.HeadOfficeIds)},
                { OrgEnumType.BD,nameof(IMOrgIevelIdsDto.DivisionIds) },
                { OrgEnumType.MarketingCenter, nameof(IMOrgIevelIdsDto.MarketingIds) },
                { OrgEnumType.SaleRegion,nameof(IMOrgIevelIdsDto.DutyregionIds) },
                { OrgEnumType.Department, nameof(IMOrgIevelIdsDto.DepartmentIds) },
                { OrgEnumType.Station, nameof(IMOrgIevelIdsDto.StationIds) },
                { OrgEnumType.Distributor, nameof(IMOrgIevelIdsDto.DistributorIds) }
            };

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="orgEnumType"></param>
        /// <param name="ids"></param>
        /// <exception cref="ArgumentException"></exception>
        public IMOrgIevelIdsDto SetIds(OrgEnumType orgEnumType, IList<Guid> ids)
        {
            if (!map.ContainsKey(orgEnumType))
            {
                throw new ArgumentException("设置属性值失败");
            }
            this.GetType().GetProperty(map[orgEnumType]).SetValue(this, ids);
            return this;
        }
    }
}
