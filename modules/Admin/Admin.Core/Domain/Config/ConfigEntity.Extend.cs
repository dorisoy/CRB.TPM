using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Sharding;

namespace CRB.TPM.Mod.Admin.Core.Domain.Config
{
    public partial class ConfigEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Ignore]
        public string TypeName => Type.ToDescription();
    }
}
