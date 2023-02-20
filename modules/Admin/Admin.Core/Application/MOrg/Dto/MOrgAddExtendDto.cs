using System;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto
{
    public class MOrgAddExtendDto : MOrgAddDto
    {
        public Guid Id { get; set; }
        public string ParentOrgCode { get; set; }
        public string ParentOrgName { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public string Deleter { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Guid CreatedBy { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public Guid? ModifiedBy { get; set; }
        public string Modifier { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
