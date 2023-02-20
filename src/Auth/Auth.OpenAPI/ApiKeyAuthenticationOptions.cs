using Microsoft.AspNetCore.Authentication;

namespace CRB.TPM.Auth.OpenAPI;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string AuthenticationScheme = "ApiKey";

    public string Realm { get; set; } = "api-tracking";

    public string Header { get; set; } = "ApiKey";

    public string QueryParam { get; set; } = "api_key";

    public string ApiKey { get; set; } = "8e421ff965cb4935ba56ef7833bf4750";
}

