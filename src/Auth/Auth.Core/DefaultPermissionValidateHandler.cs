using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Utils.Enums;

namespace CRB.TPM.Auth.Core
{
    internal class DefaultPermissionValidateHandler : IPermissionValidateHandler
    {
        public Task<bool> Validate(IDictionary<string, object> routeValues, HttpMethod httpMethod)
        {
            return Task.FromResult(true);
        }
    }
}
