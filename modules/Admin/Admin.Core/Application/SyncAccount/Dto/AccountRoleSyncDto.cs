using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto
{
    public class AccountRoleSyncDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string UserBP { get; set; }
        public Guid RoleId { get; set; }
        public string PostAttrCode { get; set; }
        public Guid OrgId { get; set; }
        public string OrgCode { get; set; }
        public string Status { get; set; }
        public Guid MinOrgId { get; set; }
        public string MinOrgCode { get; set; }
    }
}
