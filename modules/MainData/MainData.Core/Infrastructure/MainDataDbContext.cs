using CRB.TPM.Data.Core;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure;

/// <summary>
/// 这里是创建的是MainData集成模块模块数据库上下文
/// </summary>
public class MainDataDbContext : DbContext{}

/// <summary>
/// 这里是创建的是MainData集成模块模块客户端模式数据库上下文
/// </summary>
public class MainDataClientDbContext : ClientDbContext { }
