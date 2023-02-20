using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions.Annotations;

namespace CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;

public partial class AuditInfoEntity
{
    /// <summary>
    /// 平台名称
    /// </summary>
	[NotMappingColumn]
    public string PlatformName { get; set; }

}
