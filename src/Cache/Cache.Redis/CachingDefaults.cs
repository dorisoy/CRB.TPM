using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Cache.Redis;

public class CachingDefaults
{
    /// <summary>
    /// 获取用于将保护密钥列表存储到 Redis的密钥（与启用的persistDataProtectionKeysRedis选项一起使用）
    /// </summary>
    public static string RedisDataProtectionKey => "CRB.TPM.DATAPROTECTIONKEYS";
}
