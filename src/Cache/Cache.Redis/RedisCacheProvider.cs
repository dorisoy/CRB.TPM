using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Cache.Abstractions;

namespace CRB.TPM.Cache.Redis;

public class RedisCacheProvider : ICacheProvider
{
    private readonly RedisHelper _helper;

    public RedisCacheProvider(RedisHelper helper)
    {
        _helper = helper;
    }

    public Task<string> Get(string key)
    {
        return _helper.StringGetAsync<string>(key);
    }

    public Task<T> Get<T>(string key)
    {
        return _helper.StringGetAsync<T>(key);
    }

    public Task<bool> Set<T>(string key, T value)
    {
        return _helper.StringSetAsync(key, value);
    }

    public Task<bool> Set<T>(string key, T value, int expires)
    {
        return _helper.StringSetAsync(key, value, new TimeSpan(0, 0, expires, 0));
    }

    public Task<bool> Set<T>(string key, T value, DateTime expires)
    {
        return _helper.StringSetAsync(key, value, expires - DateTime.Now);
    }

    public Task<bool> Set<T>(string key, T value, TimeSpan expires)
    {
        return _helper.StringSetAsync(key, value, expires);
    }

    public Task<bool> Remove(string key)
    {
        return _helper.KeyDeleteAsync(key);
    }

    public Task<bool> Exists(string key)
    {
        return _helper.KeyExistsAsync(key);
    }

    public Task RemoveByPrefix(string prefix)
    {
        return _helper.DeleteByPrefix(prefix);
    }
    /// <summary>
    /// 批量插入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="setDatas"></param>
    /// <returns></returns>
    public Task<bool> BatchSetAsync<T>(IList<(string key, T obj, TimeSpan? expiry)> setDatas)
    {
        return _helper.StringBatchSetAsync(setDatas);
    }
    /// <summary>
    /// 批量插入带事务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="setDatas"></param>
    /// <returns></returns>
    public bool TransactionBatchSetAsync<T>(IList<(string key, T obj, TimeSpan? expiry)> setDatas)
    {
        return _helper.StringBatchSetTransaction(setDatas);
    }
    /// <summary>
    /// 批量获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyList"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IList<T>> BatchGet<T>(IList<string> keyList)
    {
        return await _helper.StringBatchGetAsync<T>(keyList);
    }
    /// <summary>
    /// 批量删除key
    /// </summary>
    /// <param name="keyList"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> BatchDeleteKeyAsync(IList<string> keyList)
    {
        return await _helper.BatchDeleteKeyAsync(keyList);
    }

    public async Task<bool> KeyAllExistsAsync(IList<string> keyList)
    {
        return await _helper.KeyAllExistsAsync(keyList);
    }

    public async Task<bool> KeyAnyExistsAsync(IList<string> keyList)
    {
        return await _helper.KeyAnyExistsAsync(keyList);
    }
}