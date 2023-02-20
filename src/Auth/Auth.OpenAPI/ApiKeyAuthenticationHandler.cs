using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CRB.TPM.Auth.OpenAPI;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
	private readonly IApiKeyAuthenticationService _authenticationService;

	public ApiKeyAuthenticationHandler(
		IOptionsMonitor<ApiKeyAuthenticationOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock,
		IApiKeyAuthenticationService authenticationService)
		: base(options, logger, encoder, clock)
	{
		_authenticationService = authenticationService;
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		string apiKey;
		if (Request.Query.ContainsKey(Options.QueryParam))
		{
			apiKey = Request.Query[Options.QueryParam];
		}
		else
		{
			if (!Request.Headers.ContainsKey(Options.Header))
			{
				return AuthenticateResult.NoResult();
			}

			if (!AuthenticationHeaderValue.TryParse(Request.Headers[Options.Header], out var headerValue))
			{
				return AuthenticateResult.NoResult();
			}

			apiKey = headerValue.ToString();
		}

		var isValidApiKey = await _authenticationService.IsValidApiKey(apiKey);

		if (!isValidApiKey)
		{
			return AuthenticateResult.Fail("Invalid API key");
		}

		var claims = new[] { new Claim(ClaimTypes.Name, "API Key user") };
		var identity = new ClaimsIdentity(claims, Scheme.Name);
		var principal = new ClaimsPrincipal(identity);
		var ticket = new AuthenticationTicket(principal, Scheme.Name);
		return AuthenticateResult.Success(ticket);
	}

	protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
	{
		Response.Headers["WWW-Authenticate"] = $"ApiKey realm=\"{Options.Realm}\", charset=\"UTF-8\"";
		await base.HandleChallengeAsync(properties);
	}
}
