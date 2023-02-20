using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Core
{
    /// <summary>
    /// 登录方式
    /// </summary>
    public class LoginModeConfig
    {
        /// <summary>
        /// 用户名登录
        /// </summary>
        public bool UserName { get; set; } = true;

        /// <summary>
        /// 邮箱登录
        /// </summary>
        public bool Email { get; set; }

        /// <summary>
        /// 用户名或邮箱登录
        /// </summary>
        public bool UserNameOrEmail { get; set; }

        /// <summary>
        /// 手机号登录
        /// </summary>
        public bool Phone { get; set; }

        /// <summary>
        /// 微信扫码登录
        /// </summary>
        public bool WeChatScanCode { get; set; }

        /// <summary>
        /// QQ登录
        /// </summary>
        public bool QQ { get; set; }

        /// <summary>
        /// GitHub
        /// </summary>
        public bool GitHub { get; set; }
    }
}
