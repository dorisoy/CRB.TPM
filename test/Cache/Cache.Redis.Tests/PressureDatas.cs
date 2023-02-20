using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Redis.Tests
{
    public class PressureDatas
    {
        private static readonly IEnumerable<object[]> _PressureRedisCacheProviderData = new List<object[]>
        {
            new object[] { "TESET_A", 10000 },
            new object[] { "TESET_B", 50000 },
        };

        public static IEnumerable<object[]> PressureRedisCacheProviderData => _PressureRedisCacheProviderData;
    }
}
