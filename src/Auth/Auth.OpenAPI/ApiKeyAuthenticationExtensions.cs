using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CRB.TPM.Auth.OpenAPI;

public static class ApiKeyAuthenticationExtensions
{
	public static AuthenticationBuilder AddApiKey<TAuthService>(this AuthenticationBuilder builder)
		where TAuthService : class, IApiKeyAuthenticationService
	{
		return AddApiKey<TAuthService>(builder, ApiKeyAuthenticationOptions.AuthenticationScheme, _ => { });
	}

	public static AuthenticationBuilder AddApiKey<TAuthService>(this AuthenticationBuilder builder, string authenticationScheme)
		where TAuthService : class, IApiKeyAuthenticationService
	{
		return AddApiKey<TAuthService>(builder, authenticationScheme, _ => { });
	}

	public static AuthenticationBuilder AddApiKey<TAuthService>(this AuthenticationBuilder builder,
		Action<ApiKeyAuthenticationOptions> configureOptions)
		where TAuthService : class, IApiKeyAuthenticationService
	{
		return AddApiKey<TAuthService>(builder, ApiKeyAuthenticationOptions.AuthenticationScheme, configureOptions);
	}

	public static AuthenticationBuilder AddApiKey<TAuthService>(this AuthenticationBuilder builder,
		string authenticationScheme,
		Action<ApiKeyAuthenticationOptions> configureOptions)
		where TAuthService : class, IApiKeyAuthenticationService
	{
		builder.Services.AddTransient<IApiKeyAuthenticationService, TAuthService>();
		builder.Services.Configure(configureOptions);

		return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
			authenticationScheme, configureOptions);
	}
}
