using CRB.TPM.Data.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Domain.JwtAuthInfo;

/// <summary>
/// JWT认证信息仓储
/// </summary>
public interface IJwtAuthInfoRepository : IRepository<JwtAuthInfoEntity>
{
}