using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto
{
    public class DistributorSelectDto : GlobalOrgFilterDto
    {
        /// <summary>
        /// 如果传了ids那么返回这些ids的信息就行 忽略分页
        /// </summary>
        public IList<Guid> Ids { get; set; }
        /// <summary>
        /// 1经销商 2分销商
        /// </summary>
        [Required]
        public int Type { get; set; }
        /// <summary>
        /// 筛选
        /// </summary>
        public string Name { get; set; }
    }
}
