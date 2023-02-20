using Cache.Redis.Tests.Models;
using Cache.Redis.Tests.XUnitExtensions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Cache.Redis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Redis.Tests.UnitTests
{
    public class RedisBatchTest
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly RedisHelper _redisHelper;
        private readonly ILogger<RedisCacheProviderTests> _logger;

        public RedisBatchTest(ICacheProvider cacheProvider, RedisHelper redisHelper, ILogger<RedisCacheProviderTests> logger)
        {
            this._cacheProvider = cacheProvider;
            this._redisHelper = redisHelper;
            this._logger = logger;
        }

        /// <summary>
        /// 获取模拟插入数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private List<(string key, SP_ConfigBusinessItemL1Entity obj, TimeSpan? expiry)> GetStringSetDatas(string code, int num)
        {
            var setDatas = new List<(string key, SP_ConfigBusinessItemL1Entity obj, TimeSpan? expiry)>();
            var setList = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            for (int i = 0; i < setList.Count; i++)
            {
                string key = "UnixTest_key" + code + (i + 1);
                setDatas.Add((key, setList[i], null));
            }
            return setDatas;
        }
        /// <summary>
        /// 获取模拟key集合
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private IList<string> GetStringKeyList(string code, int num)
        {
            return Enumerable.Range(1, num).Select(s => "UnixTest_key" + code + s).ToList();
        }

        #region 批量插入
        /// <summary>
        /// 字符串批量插入带事务
        /// </summary>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_ProviderBatchSetTransaction_Set")]
        public void String_ProviderBatchSetTransaction_Set(string code, int num)
        {
            var setDatas = GetStringSetDatas(code, num);
            var res = _cacheProvider.TransactionBatchSetAsync(setDatas);
            Assert.True(res);
        }

        /// <summary>
        /// 字符串批量插入
        /// </summary>
        /// <returns></returns>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_ProviderBatch_Set")]
        public async Task String_ProviderBatch_Set(string code, int num)
        {
            var setDatas = GetStringSetDatas(code, num);
            var res = await _cacheProvider.BatchSetAsync(setDatas);
            Assert.True(res);
        }

        /// <summary>
        /// 字符串批量插入带事务
        /// </summary>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_HelperBatchSetTransaction_Set")]
        public void String_HelperBatchSetTransaction_Set(string code, int num)
        {
            var setDatas = GetStringSetDatas(code, num);
            var res = _redisHelper.StringBatchSetTransaction(setDatas);
            Assert.True(res);
        }

        /// <summary>
        /// 字符串批量插入
        /// </summary>
        /// <returns></returns>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_HelperBatch_Set")]
        public async Task String_HelperBatch_Set(string code, int num)
        {
            var setDatas = GetStringSetDatas(code, num);
            var res = await _redisHelper.StringBatchSetAsync(setDatas);
            Assert.True(res);
        }
        #endregion

        #region 批量获取
        /// <summary>
        /// 批量获取_cacheProvider
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_ProviderBatchSetTransaction_Get")]
        public async Task String_ProviderBatchSetTransaction_Get(string code, int num)
        {
            var keyList = GetStringKeyList(code, num);
            var res = await _cacheProvider.BatchGet<SP_ConfigBusinessItemL1Entity>(keyList);
            Assert.Equal(num, res.Count);
        }
        /// <summary>
        /// 批量获取_redisHelper
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "String_HelperBatchSetTransaction_Get")]
        public async Task String_HelperBatchSetTransaction_Get(string code, int num)
        {
            var keyList = GetStringKeyList(code, num);
            var res = await _redisHelper.StringBatchGetAsync<SP_ConfigBusinessItemL1Entity>(keyList);
            Assert.Equal(num, res.Count);
        }
        #endregion

        #region 批量删除Key
        /// <summary>
        /// 批量删除Key，_cacheProvider
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(2)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Key", "BatchProviderDeleteKeyAsync")]
        public async Task BatchProviderDeleteKeyAsync(string code, int num)
        {
            var keyList = GetStringKeyList(code, num);
            var res = await _cacheProvider.BatchDeleteKeyAsync(keyList);
            Assert.True(res);
        }
        /// <summary>
        /// 批量删除Key，_redisHelper
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(2)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Key", "BatchHelperDeleteKeyAsync")]
        public async Task BatchHelperDeleteKeyAsync(string code, int num)
        {
            var keyList = GetStringKeyList(code, num);
            var res = await _redisHelper.BatchDeleteKeyAsync(keyList);
            Assert.True(res);
        }
        #endregion

        #region Key扩展
        /// <summary>
        /// 全包含，_cacheProvider
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(2)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Key", "BatchProviderAllExistsAsync")]
        public async Task BatchProviderAllExistsAsync(string code, int num)
        {
            var keyList = GetStringKeyList(code, num / 100);
            var res = await _cacheProvider.KeyAllExistsAsync(keyList);
            Assert.True(res);
        }
        /// <summary>
        /// 任意包含，_cacheProvider
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(2)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Key", "BatchProviderAnyExistsAsync")]
        public async Task BatchProviderAnyExistsAsync(string code, int num)
        {
            var keyList = GetStringKeyList(code, 2);
            keyList[1] = "test";
            var res = await _cacheProvider.KeyAnyExistsAsync(keyList);
            Assert.True(res);
        }
        #endregion
    }
}
