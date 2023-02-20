using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.LoginHandlers;
using CRB.TPM.Auth.Abstractions.Options;
using CRB.TPM.Auth.Jwt;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
namespace CRB.TPM.Mod.Admin.Core.Application.Authorize;

public class AuthorizeService : IAuthorizeService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountResolver _accountResolver;
    private readonly ICredentialClaimExtender _credentialClaimExtender;
    private readonly ICredentialBuilder _credentialBuilder;
    private readonly IJwtTokenStorage _jwtTokenStorageProvider;
    private readonly IUsernameLoginHandler _usernameLoginHandler;
    private readonly IOptionsMonitor<AuthOptions> _authOptions;
    private readonly AdminLocalizer _localizer;



    public AuthorizeService(IAccountRepository accountRepository,
        IAccountResolver accountResolver,
        ICredentialClaimExtender credentialClaimExtender,
        ICredentialBuilder credentialBuilder, 
        IJwtTokenStorage jwtTokenStorageProvider,
        IUsernameLoginHandler usernameLoginHandler,
        IOptionsMonitor<AuthOptions> authOptions,
        AdminLocalizer localizer)
    {
        _accountRepository = accountRepository;
        _accountResolver = accountResolver;
        _credentialClaimExtender = credentialClaimExtender;
        _credentialBuilder = credentialBuilder;
        _jwtTokenStorageProvider = jwtTokenStorageProvider;
        _usernameLoginHandler = usernameLoginHandler;
        _authOptions = authOptions;
        _localizer = localizer;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel<ICredential>> UsernameLogin(UsernameLoginModel model)
    {
        var result = await _usernameLoginHandler.Handle(model);
        if (!result.Successful)
            return ResultModel.Failed<ICredential>(result.Msg);

        var loginResult = result.Data;

        var claims = new List<Claim>
        {
            new(CRBTPMClaimTypes.TENANT_ID, loginResult.TenantId != null ? loginResult.TenantId.ToString() : ""),
            new(CRBTPMClaimTypes.ACCOUNT_ID, loginResult.AccountId.ToString()),
            new(CRBTPMClaimTypes.ACCOUNT_NAME, loginResult.AccountName),
            new(CRBTPMClaimTypes.PLATFORM, model.Platform.ToString()),
            new(CRBTPMClaimTypes.LOGIN_TIME, model.LoginTime.ToString())
        };

        //验证IP
        if (_authOptions.CurrentValue.EnableCheckIP)
        {
            claims.Add(new(CRBTPMClaimTypes.LOGIN_IP, model.IP));
        }

        if (_credentialClaimExtender != null)
        {
            await _credentialClaimExtender.Extend(claims, loginResult.AccountId);
        }

        return ResultModel.Success(await _credentialBuilder.Build(claims));
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel<JwtCredential>> RefreshToken(RefreshTokenDto dto)
    {
        var accountId = await _jwtTokenStorageProvider.CheckRefreshToken(dto.RefreshToken, dto.Platform);
        if (accountId != Guid.Empty)
        {
            var account = await _accountRepository.Get(accountId);
            var claims = new List<Claim>
            {
                new(CRBTPMClaimTypes.TENANT_ID, account.TenantId != null ? account.TenantId.ToString() : ""),
                new(CRBTPMClaimTypes.ACCOUNT_ID, account.Id.ToString()),
                new(CRBTPMClaimTypes.ACCOUNT_NAME, account.Name),
                new(CRBTPMClaimTypes.PLATFORM, dto.Platform.ToInt().ToString()),
                new(CRBTPMClaimTypes.LOGIN_TIME, DateTime.Now.ToTimestamp().ToString()),
                new(CRBTPMClaimTypes.LOGIN_IP, dto.IP)
            };

            if (account.TenantId != null)
            {
            }

            if (_credentialClaimExtender != null)
            {
                await _credentialClaimExtender.Extend(claims, account.Id);
            }

            var jwtCredential = (JwtCredential)await _credentialBuilder.Build(claims);
            jwtCredential.RefreshToken = dto.RefreshToken;

            return ResultModel.Success(jwtCredential);
        }

        return ResultModel.Failed<JwtCredential>(_localizer["令牌无效"]);
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public async Task<IResultModel<ProfileVo>> GetProfile(Guid accountId, int platform)
    {
        var account = await _accountRepository.Get(accountId);
        if (account == null)
            return ResultModel.Failed<ProfileVo>(_localizer["账户不存在"]);

        if (account.Status == AccountStatus.Disabled)
            return ResultModel.Failed<ProfileVo>(_localizer["账户已禁用，请联系管理员"]);

        var profile = await _accountResolver.Resolve(account, platform);

        return ResultModel.Success(profile);
    }
}