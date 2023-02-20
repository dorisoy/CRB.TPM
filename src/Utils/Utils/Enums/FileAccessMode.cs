using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Enums
{
    /// <summary>
    /// 文件访问方式
    /// </summary>
    public enum FileAccessMode
    {
        /// <summary>
        /// 私有
        /// </summary>
        [Description("私有")]
        Private,
        /// <summary>
        /// 公开
        /// </summary>
        [Description("公开")]
        Open,
        /// <summary>
        /// 授权
        /// </summary>
        [Description("授权")]
        Auth
    }
}
