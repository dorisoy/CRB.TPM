using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo
{
    [ObjectMap(typeof(MDistributorRelationEntity), true)]
    public class MDistributorRelationEditVo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string DistributorCode1 { get; set; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string DistributorCode2 { get; set; }
        /// <summary>
        /// 经销商id
        /// </summary>
        public Guid DistributorId1 { get; set; }
        /// <summary>
        /// 分销商id
        /// </summary>
        public Guid DistributorId2 { get; set; }
    }
}
