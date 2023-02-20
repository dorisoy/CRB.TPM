using CRB.TPM.Config.Abstractions;
using CRB.TPM.Module.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRB.TPM.Config.Core.Provider;

/// <summary>
/// 默认配置提供
/// </summary>
public class DefaultConfigProvider : IConfigProvider
{
    /// <summary>
    /// 表示含有实例的配置描述符集合
    /// </summary>
    private readonly Dictionary<RuntimeTypeHandle, IConfig> _configs = new();
    private readonly IConfiguration _cfg;

    /// <summary>
    /// 含有了实例的配置描述符集合(已过时)
    /// </summary>
    private static readonly IConfigCollection ConfigWidthInstanceCollection = new ConfigCollection();

    private readonly IConfigCollection _configCollection;
    private readonly IConfigStorageProvider _storageProvider;
    private readonly IServiceProvider _serviceProvider;

    public DefaultConfigProvider(IConfiguration cfg)
    {
        _cfg = cfg;
    }

    public DefaultConfigProvider(IConfiguration cfg,
        IConfigCollection configCollection,
        IConfigStorageProvider storageProvider,
        IServiceProvider serviceProvider) : this(cfg)
    {
        _configCollection = configCollection;
        _storageProvider = storageProvider;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取指定的配置实例
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <returns></returns>
    public TConfig Get<TConfig>() where TConfig : IConfig, new()
    {
        var item = _configs.FirstOrDefault(m => m.Key.Value == typeof(TConfig).TypeHandle.Value);
        return (TConfig)item.Value;
    }

    /// <summary>
    /// 获取指定的配置实例
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <returns></returns>
    public TConfig GetFor<TConfig>() where TConfig : IConfig, new()
    {
        var descriptor = GetDescriptor(m => m.ImplementType == typeof(TConfig));
        if (descriptor.Instance == null)
        {
            var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
            if (json.IsNull())
            {
                descriptor.Instance = new TConfig();
                if (descriptor.Type == ConfigType.Library)
                {
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
            }
            else
                descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);
        }

        return (TConfig)descriptor.Instance;
    }

    /// <summary>
    /// 获取指定实现类的实例
    /// </summary>
    /// <param name="implementType"></param>
    /// <returns></returns>
    public IConfig Get(Type implementType)
    {
        var descriptor = GetDescriptor(m => m.ImplementType == implementType);
        if (descriptor.Instance == null)
        {
            var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
            if (json.IsNull())
            {
                descriptor.Instance = (IConfig)Activator.CreateInstance(implementType);
                if (descriptor.Type == ConfigType.Library)
                {
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
            }
            else
                descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, implementType);
        }

        return descriptor.Instance;
    }

    /// <summary>
    /// 获取配置实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public IConfig Get(string code, ConfigType type)
    {
        var descriptor = GetDescriptor(m => m.Code.EqualsIgnoreCase(code) && m?.Type == type);
        if (descriptor.Instance == null)
        {
            var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
            if (json.IsNull())
            {
                descriptor.Instance = (IConfig)Activator.CreateInstance(descriptor.ImplementType);
                if (descriptor.Type == ConfigType.Library)
                {
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
            }
            else
                descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);
        }

        return descriptor.Instance;
    }

    /// <summary>
    /// 设置配置类
    /// </summary>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public bool Set(ConfigType type, string code, string json)
    {
        if (json.IsNull())
            return false;

        var descriptor = GetDescriptor(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
        //旧实例
        var oldInstance = descriptor.Instance;
        //新实例
        descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);

        //持久化
        _storageProvider.SaveJson(type, descriptor.Code, json);

        #region ==触发变更事件==

        foreach (var changeEventType in descriptor.ChangeEvents)
        {
            var eventInstance = _serviceProvider.GetService(changeEventType);
            var method = changeEventType.GetMethod("OnChanged");
            if (method != null)
            {
                method.Invoke(eventInstance, new object[] { descriptor.Instance, oldInstance });
            }
        }

        #endregion

        return true;
    }

    /// <summary>
    /// 添加模块配置
    /// </summary>
    /// <param name="modules"></param>
    public void AddModuleConfig(IModuleCollection modules)
    {
        foreach (var module in modules)
        {
            var configType = module.LayerAssemblies.Core.GetTypes().FirstOrDefault(m => typeof(IConfig).IsImplementType(m));
            if (configType != null)
            {
                //模块配置节点
                var instance = (IConfig)Activator.CreateInstance(configType)!;
                _cfg.GetSection($"CRB.TPM:Modules:{module.Code}:Config").Bind(instance);
                if (!_configs.Select(s => s.Key).Contains(configType.TypeHandle))
                    _configs.Add(configType.TypeHandle, instance);
            }
        }
    }

    /// <summary>
    /// 获取描述信息
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private ConfigDescriptor GetDescriptor(Func<ConfigDescriptor, bool> predicate)
    {
        ConfigDescriptor descriptor = null;
        if (predicate != null)
        {
            descriptor = ConfigWidthInstanceCollection.FirstOrDefault(c => predicate(c));
            //descriptor = ConfigWidthInstanceCollection.FirstOrDefault();
        }

        if (descriptor == null)
        {
            descriptor = _configCollection.Where(predicate).Select(m => new ConfigDescriptor
            {
                Type = m.Type,
                Code = m.Code,
                ImplementType = m.ImplementType,
                ChangeEvents = m.ChangeEvents
            }).FirstOrDefault();

            ConfigWidthInstanceCollection.Add(descriptor);
        }

        if (descriptor == null)
            throw new NullReferenceException("指定类型的配置实例不存在");

        return descriptor;
    }
}