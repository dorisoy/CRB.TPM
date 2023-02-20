using System;
using System.Threading.Tasks;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.LoginHandlers;
using CRB.TPM.Auth.Jwt;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;

namespace CRB.TPM.Mod.Admin.Core.Application.Authorize;

/// <summary>
/// 认证服务
/// </summary>
public interface IAuthorizeService
{
    /// <summary>
    /// 用户名登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IResultModel<ICredential>> UsernameLogin(UsernameLoginModel model);

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel<JwtCredential>> RefreshToken(RefreshTokenDto dto);

    /// <summary>
    /// 获取指定账户的个人信息
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="platform">登录平台</param>
    /// <returns></returns>
    Task<IResultModel<ProfileVo>> GetProfile(Guid accountId, int platform);
}