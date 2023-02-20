using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions;
using System.Collections.Generic;

namespace CRB.TPM.Module.Abstractions;

/// <summary>
///  模块服务
/// </summary>

public interface IService
{

}


/// <summary>
/// 模块集合
/// </summary>
public interface IModuleCollection : IList<ModuleDescriptor>
{
    /// <summary>
    /// 根据模块编号获取模块信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ModuleDescriptor Get(int id);

    /// <summary>
    /// 根据模块编码获取模块信息
    /// <para>不区分大小写</para>
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    ModuleDescriptor Get(string code);
}