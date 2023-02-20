using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto
{

    /// <summary>
    /// 用于表示全局组织过滤器
    /// </summary>
    public abstract class GlobalOrgFilterDto : QueryDto
    {
        /// <summary>
        /// 客户id
        /// </summary>
        public Guid DistributorId { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public int DistributorType { get; set; }

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
        /// 经销商编码ids
        /// </summary>
        public IList<Guid> DistributorIds { get; set; }
    
    }


    public static class GlobalOrgFilterDtoExt
    {
        public static async Task<string> BuildFilter(this GlobalOrgFilterDto dto, IServiceProvider sp, OrgEnumType orgEnum = OrgEnumType.Station, string objAlias = "mobj")
        {
            if (sp != null)
            {
                var accountResolver = sp.GetService<IAccountResolver>();
                return await accountResolver.BuildSqlWhereStrByOrgAuth(dto, orgEnum, objAlias);
            }
            else
                return string.Empty;
        }
    }
}
