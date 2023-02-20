
using CRB.TPM.Data.Abstractions.Entities;
using System;
using System.ComponentModel;

namespace CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation
{
    public partial class MDistributorRelationEntity
    {
        
    }

    public enum DistributorTypeEnum
    {
        [Description("经销商")]
        Distributor1 = 1,
        [Description("分销商")]
        Distributor2 = 2
    }
}
