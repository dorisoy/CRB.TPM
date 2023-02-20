using CRB.TPM.Utils.Enums;
using CRB.TPM.Utils.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using CRB.TPM.Module.Abstractions.Options;


namespace CRB.TPM.Module.Web.Attributes;


/// <summary>
/// 表示一个筛选器属性，用于检查当前连接是否安全，并在必要时正确重定向  
/// </summary>
public class HttpsRequirementAttribute : TypeFilterAttribute
{

    private readonly SslRequirement _sslRequirement;

    /// <summary>
    /// 创建筛选器属性的实例
    /// </summary>
    /// <param name="sslRequirement">是否应保护页面</param>
    public HttpsRequirementAttribute(SslRequirement sslRequirement) : base(typeof(HttpsRequirementFilter))
    {
        _sslRequirement = sslRequirement;
        Arguments = new object[] { sslRequirement };
    }


    /// <summary>
    /// 表示是否应保护页面
    /// </summary>
    public SslRequirement SslRequirement => _sslRequirement;


    /// <summary>
    /// 表示一个筛选器，用于确认当前连接是否安全，并在必要时正确重定向
    /// </summary>
    private class HttpsRequirementFilter : IAuthorizationFilter
    {

        private SslRequirement _sslRequirement;
        private readonly IWebHelper _webHelper;
        private readonly IOptionsMonitor<CommonOptions> _options;


        public HttpsRequirementFilter(SslRequirement sslRequirement,
            IWebHelper webHelper,
            IOptionsMonitor<CommonOptions> options)
        {
            _sslRequirement = sslRequirement;
            _webHelper = webHelper;
            _options = options;
        }


        protected void RedirectRequest(AuthorizationFilterContext filterContext, bool useSsl)
        {
            //当前连接是否安全
            var currentConnectionSecured = _webHelper.IsCurrentConnectionSecured();

            //页面受到保护，则重定向（永久）到HTTPS版本
            if (useSsl && !currentConnectionSecured)
            {
                filterContext.Result = new RedirectResult(_webHelper.GetThisPageUrl(true, true), true);
            }

            //页面不受到保护，则重定向（永久）到页面的HTTP版本
            if (!useSsl && currentConnectionSecured)
            {
                filterContext.Result = new RedirectResult(_webHelper.GetThisPageUrl(true, false), true);
            }
        }



        /// <summary>
        /// 在筛选器管道的早期调用以确认请求已授权
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.HttpContext.Request == null)
            {
                return;
            }

            //仅在GET请求中，否则浏览器可能无法正确传播谓词和请求正文
            if (!filterContext.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Get, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            //检查此筛选器是否已为操作重写
            var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                .Select(filterDescriptor => filterDescriptor.Filter).OfType<HttpsRequirementAttribute>().FirstOrDefault();

            var sslRequirement = actionFilter?.SslRequirement ?? _sslRequirement;

            //无论传递的值如何，是否强制所有页面使用SSL (生产环境应该强制开启)
            //_options.CurrentValue.ForceSslForAllPages = true;

            if (_options.CurrentValue.ForceSslForAllPages)
            {
                sslRequirement = SslRequirement.Yes;
            }

            switch (sslRequirement)
            {
                case SslRequirement.Yes:
                    //重定向到 HTTPS 页面
                    RedirectRequest(filterContext, true);
                    break;
                case SslRequirement.No:
                    //重定向到 HTTP 页面
                    RedirectRequest(filterContext, false);
                    break;
                case SslRequirement.NoMatter:
                    break;
                default:
                    throw new Exception("不支持SslRequirement参数");
            }
        }
    }
}
