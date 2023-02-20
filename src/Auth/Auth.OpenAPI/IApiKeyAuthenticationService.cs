using System.Threading.Tasks;

namespace CRB.TPM.Auth.OpenAPI;

public interface IApiKeyAuthenticationService
{
    Task<bool> IsValidApiKey(string apiKey);
}