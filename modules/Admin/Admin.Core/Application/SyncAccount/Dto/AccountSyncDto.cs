using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto
{
    public class AccountSyncDto
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public string OrgCode { get; set; }
        public string UserBP { get; set; }
        public string LDAPName { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
