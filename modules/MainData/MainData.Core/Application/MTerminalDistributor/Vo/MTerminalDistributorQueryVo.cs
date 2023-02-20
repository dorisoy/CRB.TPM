using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Vo
{
    public class MTerminalDistributorQueryVo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 终端id
        /// </summary>
        public Guid TerminalId { get; set; }

        /// <summary>
        /// 经销商id
        /// </summary>
        public Guid DistributorId { get; set; }

        /// <summary>
        /// 终端编码
        /// </summary>
        public string TerminalCode { get; set; }

        /// <summary>
        /// 终端名称
        /// </summary>
        public string TerminalName { get; set; }

        /// <summary>
        /// 经销商编码
        /// </summary>
        public string DistributorCode { get; set; }

        /// <summary>
        /// 经销商名称
        /// </summary>
        public string DistributorName { get; set; }
    }
}
