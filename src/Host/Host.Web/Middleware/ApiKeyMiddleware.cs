using CRB.TPM.Config.Abstractions;
using CRB.TPM.Config.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CRB.TPM.Host.Web.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "ApiKey";
    //private readonly IConfigProvider _configProvider;
    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key was not provided");
            return;
        }

        //var apiKey = _configProvider.GetValue<string>(APIKEY);
        var _configProvider = context.RequestServices.GetRequiredService<IConfigProvider>();
        var config = _configProvider.Get<AuthConfig>();
        var apiKey = config.ApiKey;

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }

        await _next(context);
    }
}
