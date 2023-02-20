using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Options;

namespace CRB.TPM.Data.Abstractions;

/// <summary>
/// 代码优先提供器
/// </summary>
public interface ICodeFirstProvider
{
    /// <summary>
    /// 创建库
    /// </summary>
    void CreateDatabase();

    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="entity"></param>
    void CreateTable(IEntity entity = null);

    /// <summary>
    /// 创建表
    /// </summary>
    void CreateNextTable();

    /// <summary>
    /// CodeFirst配置项
    /// </summary>
    /// <returns></returns>
    CodeFirstOptions GetOptions();
}