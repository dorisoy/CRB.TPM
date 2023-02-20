using CRB.TPM.Config.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Core
{
    /// <summary>
    /// 身份认证和授权配置
    /// </summary>
    public class AuthConfig : IConfig
    {
        /// <summary>
        /// 启用验证码功能
        /// </summary>
        public bool VerifyCode { get; set; }

        /// <summary>
        /// 开启权限验证
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// 开启按钮权限
        /// </summary>
        public bool Button { get; set; }

        /// <summary>
        /// 单账户登录
        /// </summary>
        public bool SingleAccount { get; set; }

        /// <summary>
        /// 启用审计日志
        /// </summary>
        public bool Auditing { get; set; }

        /// <summary>
        /// Jwt配置
        /// </summary>
        public JwtConfig Jwt { get; set; } = new JwtConfig();

        /// <summary>
        /// Api访问秘钥(生产环境需要策略动态生成)
        /// </summary>
        public string ApiKey { get; set; } = "P5K35mrECDevGvFEqNx2p8jYZzEAJjwC8LUUmmA2EX2yKJgQsXES";

        /// <summary>
        /// 登录
        /// </summary>
        public LoginModeConfig LoginMode { get; set; } = new LoginModeConfig();
    }

}
