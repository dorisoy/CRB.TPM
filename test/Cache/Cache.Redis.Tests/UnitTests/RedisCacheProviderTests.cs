using Cache.Redis.Tests.Models;
using Cache.Redis.Tests.XUnitExtensions;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Cache.Redis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Cache.Redis.Tests.UnitTests
{
    public class RedisCacheProviderTests
    {
        public RedisCacheProviderTests(ICacheProvider cacheProvider, ITestOutputHelperAccessor testOutputHelperAccessor, ILogger<RedisCacheProviderTests> logger)
        {
            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        private readonly ICacheProvider _cacheProvider;
        private readonly ILogger _logger;

        /// <summary>
        /// 压测Redis字符串类型，泛型插入
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(1)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "Pressure_Set_T")]
        public async Task String_Pressure_Set_T(string code, int num)
        {
            var setList = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            int errNum = 0;
            await _logger.WatchScopeInvokeAsync(async () =>
            {
                for (int i = 0; i < setList.Count; i++)
                {
                    var setRes = await _cacheProvider.Set("UnixTest_key" + code + i, setList[i]);
                    if (!setRes)
                    {
                        errNum++;
                        _logger.LogError("插入Reids失败" + setList[i].Code);
                    }
                }
            });
            Assert.Equal(0, errNum);
        }
        /// <summary>
        /// 压测Redis字符串类型，泛型获取对象
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(2)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "Get_T")]
        public async Task String_Pressure_Get_T(string code, int num)
        {
            var setList = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            var errNum = 0;
            await _logger.WatchScopeInvokeAsync(async () =>
            {
                for (int i = 0; i < num; i++)
                {
                    var key = "UnixTest_key" + code + i;
                    var res = await _cacheProvider.Get<SP_ConfigBusinessItemL1Entity>(key);
                    Assert.NotNull(res);
                    if (res == null)
                    {
                        errNum++;
                        _logger.LogError("没有获取值：" + key);
                    }
                }
            });
            Assert.Equal(0, errNum);
        }
        /// <summary>
        /// 压测Redis字符串类型，泛型删除
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [Theory, Order(3)]
        [MemberData(nameof(PressureDatas.PressureRedisCacheProviderData),
            MemberType = typeof(PressureDatas))]
        [Trait("String", "Pressure_Remove_T")]
        public async Task String_Pressure_Remove_T(string code, int num)
        {
            var errNum = 0;
            await _logger.WatchScopeInvokeAsync(async () =>
            {
                for (int i = 0; i < num; i++)
                {
                    var key = "UnixTest_key" + code + i;
                    var res = await _cacheProvider.Remove(key);
                    if (!res)
                    {
                        errNum++;
                        _logger.LogError("删除Key失败：" + key);
                    }
                }
            });
        }

        [Fact, Order(1)]
        [Trait("String", "Set")]
        public async Task String_Set()
        {
            var setRes = await _cacheProvider.Set("UnixTest_key", "测试");
            Assert.True(setRes);
        }

        [Fact, Order(2)]
        [Trait("String", "Remve")]
        public async Task String_Remve()
        {
            var remRes = await _cacheProvider.Remove("UnixTest_key");
            Assert.True(remRes);
        }
    }
}
