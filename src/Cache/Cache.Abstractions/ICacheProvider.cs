using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Cache.Abstractions;

/// <summary>
/// 缓存提供器
/// </summary>
public interface ICacheProvider
{
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    Task<string> Get(string key);

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    Task<T> Get<T>(string key);

    /// <summary>
    /// 批量获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyList">键集合</param>
    /// <returns></returns>
    Task<IList<T>> BatchGet<T>(IList<string> keyList);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    Task<bool> Set<T>(string key, T value);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期(分钟)</param>
    /// <returns></returns>
    Task<bool> Set<T>(string key, T value, int expires);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期，截止时间</param>
    /// <returns></returns>
    Task<bool> Set<T>(string key, T value, DateTime expires);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期，截止时间</param>
    /// <returns></returns>
    Task<bool> Set<T>(string key, T value, TimeSpan expires);

    /// <summary>
    /// 批量设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="setDatas"></param>
    /// <returns></returns>
    Task<bool> BatchSetAsync<T>(IList<(string key, T obj, TimeSpan? expiry)> setDatas);
    /// <summary>
    /// 事务批量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="setDatas"></param>
    /// <returns></returns>
    bool TransactionBatchSetAsync<T>(IList<(string key, T obj, TimeSpan? expiry)> setDatas);
    /// <summary>
    /// 批量删除key
    /// </summary>
    /// <param name="keyList"></param>
    /// <returns></returns>
    Task<bool> BatchDeleteKeyAsync(IList<string> keyList);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> Remove(string key);

    /// <summary>
    /// 指定键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> Exists(string key);

    /// <summary>
    /// 是否包含所有键
    /// </summary>
    /// <param name="keyList"></param>
    /// <returns></returns>
    Task<bool> KeyAllExistsAsync(IList<string> keyList);
    /// <summary>
    /// 是否包含所有键
    /// </summary>
    /// <param name="keyList"></param>
    /// <returns></returns>
    Task<bool> KeyAnyExistsAsync(IList<string> keyList);

    /// <summary>
    /// 删除指定前缀的缓存
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    Task RemoveByPrefix(string prefix);
}