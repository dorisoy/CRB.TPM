using Microsoft.AspNetCore.Http;

namespace CRB.TPM.Utils.Web.Helpers;

public interface IWebHelper
{
    string CurrentRequestProtocol { get; }
    bool IsRequestBeingRedirected { get; }
    string GetCurrentIpAddress();
    string GetRawUrl(HttpRequest request);
    string GetSiteHost(bool useSsl);
    string GetSiteLocation(bool? useSsl = null);
    string GetThisPageUrl(bool includeQueryString, bool? useSsl = null, bool lowercaseUrl = false);
    string GetUrlReferrer();
    bool IsAjaxRequest(HttpRequest request);
    bool IsCurrentConnectionSecured();
    bool IsLocalRequest(HttpRequest req);
    bool IsStaticResource();
    string ModifyQueryString(string url, string key, params string[] values);
    T QueryString<T>(string name);
    string RemoveQueryString(string url, string key, string value = null);
}