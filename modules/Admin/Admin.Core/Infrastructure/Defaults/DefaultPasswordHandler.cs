using System;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Encrypt;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

[SingletonInject]
internal class DefaultPasswordHandler : IPasswordHandler
{
    private readonly Md5Encrypt _encrypt;
    private const string KEY = "CRBTPM_";

    public DefaultPasswordHandler(Md5Encrypt encrypt)
    {
        _encrypt = encrypt;
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string Encrypt(string password)
    {
        return _encrypt.Encrypt(KEY + password);
    }

    public string Decrypt(string encryptPassword)
    {
        throw new NotSupportedException("MD5加密无法解密~");
    }
}