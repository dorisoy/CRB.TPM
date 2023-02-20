using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Vo
{
    public class MTerminalUserQueryVo
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 终端id
        /// </summary>
        public Guid TerminalId { get; set; }
        /// <summary>
        /// 终端编码
        /// </summary>
        public string TerminalCode { get; set; }
        /// <summary>
        /// 终端名称
        /// </summary>
        public string TerminalName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string UserBP { get; set; }
        /// <summary>
        /// 业务员id
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 业务员名字
        /// </summary>
        public string AccountName { get; set; }
    }
}
