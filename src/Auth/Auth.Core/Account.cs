using System;
using Microsoft.AspNetCore.Http;
using CRB.TPM.Auth.Abstractions;
using System.Collections.Generic;
using System.Dynamic;

namespace CRB.TPM.Auth.Core;

/// <summary>
/// 账户信息
/// </summary>
internal class Account : IAccount
{
    private readonly IHttpContextAccessor _contextAccessor;

    public Account(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid? TenantId
    {
        get
        {
            var tenantId = _contextAccessor?.HttpContext?.User.FindFirst(CRBTPMClaimTypes.TENANT_ID);

            if (tenantId != null && tenantId.Value.NotNull())
            {
                return new Guid(tenantId.Value);
            }

            return null;
        }
    }

    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid Id
    {
        get
        {
            var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(CRBTPMClaimTypes.ACCOUNT_ID);

            if (accountId != null && accountId.Value.NotNull())
            {
                return new Guid(accountId.Value);
            }

            return Guid.Empty;
        }
    }

    public string AccountName
    {
        get
        {
            var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(CRBTPMClaimTypes.ACCOUNT_NAME);

            if (accountName == null || accountName.Value.IsNull())
            {
                return "";
            }

            return accountName.Value;
        }
    }

    /// <summary>
    /// 请求平台
    /// </summary>
    public int Platform
    {
        get
        {
            var pt = _contextAccessor?.HttpContext?.User?.FindFirst(CRBTPMClaimTypes.PLATFORM);
            if (pt != null && pt.Value.NotNull())
            {
                return pt.Value.ToInt();
            }

            return 0;
        }
    }

    /// <summary>
    /// 获取当前用户IP(包含IPv和IPv6)
    /// </summary>
    public string IP
    {
        get
        {
            if (_contextAccessor?.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

    /// <summary>
    /// 获取当前用户IPv4
    /// </summary>
    public string IPv4
    {
        get
        {
            if (_contextAccessor?.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }

    /// <summary>
    /// 获取当前用户IPv6
    /// </summary>
    public string IPv6
    {
        get
        {
            if (_contextAccessor?.HttpContext?.Connection.RemoteIpAddress == null)
                return "";

            return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
        }
    }

    /// <summary>
    /// 登录时间
    /// </summary>
    public long LoginTime
    {
        get
        {
            var ty = _contextAccessor?.HttpContext?.User?.FindFirst(CRBTPMClaimTypes.LOGIN_TIME);

            if (ty != null && ty.Value.NotNull())
            {
                return ty.Value.ToLong();
            }

            return 0L;
        }
    }

    /// <summary>
    /// User-Agent
    /// </summary>
    public string UserAgent
    {
        get
        {
            if (_contextAccessor?.HttpContext?.Request == null)
                return "";

            return _contextAccessor.HttpContext.Request.Headers["User-Agent"];
        }
    }

    /// <summary>
    /// 当前访问页面的路由
    /// </summary>
    public string RouterName
    {
        get
        {
            var routerName = _contextAccessor?.HttpContext?.Request.Headers[CRBTPMClaimTypes.ROUTER_NAME];
            if (routerName.Value.IsNullOrEmpty())
            {
                return "";
            }

            return routerName.Value;
        }
    }

}