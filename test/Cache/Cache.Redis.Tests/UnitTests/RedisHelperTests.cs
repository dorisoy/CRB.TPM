using Cache.Redis.Tests.Models;
using Cache.Redis.Tests.XUnitExtensions;
using CRB.TPM.Cache.Redis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Redis.Tests.UnitTests
{
    public class RedisHelperTests
    {
        private readonly RedisHelper _redisHelper;
        private readonly ILogger<RedisCacheProviderTests> _logger;

        public RedisHelperTests(RedisHelper redisHelper, ILogger<RedisCacheProviderTests> logger)
        {
            this._redisHelper = redisHelper;
            this._logger = logger;
        }

        [Theory]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Hash", "Set_T")]
        public async Task Hash_Set(string code, int num)
        {
            var setList = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            var errNum = 0;
            var typeSp = typeof(SP_ConfigBusinessItemL1Entity);
            var props = typeSp.GetProperties();
            await _logger.WatchScopeInvokeAsync(async () =>
            {
                for (int i = 0; i < num; i++)
                {
                    foreach (var prop in props)
                    {
                        var obj = prop.GetValue(setList[i]);
                        var key = "SP_ConfigBusinessItemL1Entity";
                        var fild = $"{prop.Name}:{code}{i}";
                        if (!await _redisHelper.HashExistsAsync(key, fild))
                        {
                            var res = await _redisHelper.HashSetAsync<object?>(key, fild, obj);
                            if (!res)
                            {
                                errNum++;
                                _logger.LogError($"插入失败：{key}:{fild}");
                            }
                        }
                    }
                }
            });
            Assert.Equal(0, errNum);
        }

        [Theory]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("Hash", "Remove_T")]
        public async Task Hash_Remove(string code, int num)
        {
            var setList = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            var errNum = 0;
            var typeSp = typeof(SP_ConfigBusinessItemL1Entity);
            var props = typeSp.GetProperties();
            await _logger.WatchScopeInvokeAsync(async () =>
            {
                for (int i = 0; i < num; i++)
                {
                    foreach (var prop in props)
                    {
                        var obj = prop.GetValue(setList[i]);
                        var key = "SP_ConfigBusinessItemL1Entity";
                        var fild = $"{prop.Name}:{code}{i}";
                        if (await _redisHelper.HashExistsAsync(key, fild))
                        {
                            var res = await _redisHelper.HashDeleteAsync(key, fild);
                            if (!res)
                            {
                                errNum++;
                                _logger.LogError($"插入失败：{key}:{fild}");
                            }
                        }
                    }
                }
            });
            Assert.Equal(0, errNum);
        }
    }
}
