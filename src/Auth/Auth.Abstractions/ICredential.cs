using CRB.TPM.Utils.Json.Converters;

namespace CRB.TPM.Auth.Abstractions
{
    /// <summary>
    /// 凭据
    /// </summary>
    [JsonPolymorphism]
    public interface ICredential
    {
    }
}
