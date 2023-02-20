using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Core
{
    /// <summary>
    /// JWT配置
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; } = "twAJ$j5##pVc5*y&";

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; } = "http://127.0.0.1:6220";

        /// <summary>
        /// 消费者
        /// </summary>
        public string Audience { get; set; } = "http://127.0.0.1:6220";

        /// <summary>
        /// 有效期(分钟，默认120)
        /// </summary>
        public int Expires { get; set; } = 120;

        /// <summary>
        /// 刷新令牌有效期(单位：天，默认7)
        /// </summary>
        public int RefreshTokenExpires { get; set; } = 7;
    }

}
