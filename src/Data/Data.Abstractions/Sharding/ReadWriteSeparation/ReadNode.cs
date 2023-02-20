using CRB.TPM.Data.Abstractions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{

    /// <summary>
    /// 用于表示一个节点
    /// </summary>
    public class Node
    {
        /// <summary>
        /// 当前读库节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 当前库链接的连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }

}
