
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
        [Description("������")]
        Distributor1 = 1,
        [Description("������")]
        Distributor2 = 2
    }
}
