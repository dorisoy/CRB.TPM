using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Vo
{
    public class MDistributorRelationQueryVo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 经销商编码
        /// </summary>
        public string DistributorCode1 { get; set; }
        /// <summary>
        /// 经销商名称
        /// </summary>
        public string DistributorName1 { get; set; }
        /// <summary>
        /// 分销商编码
        /// </summary>
        public string DistributorCode2 { get; set; }
        /// <summary>
        /// 分销商名称
        /// </summary>
        public string DistributorName2 { get; set; }
        /// <summary>
        /// 经销商所属工作站名称
        /// </summary>
        public string DistributorStationName1 { get; set; }
        /// <summary>
        /// 分销商所属工作站名称
        /// </summary>
        public string DistributorStationName2 { get; set; }
    }
}
