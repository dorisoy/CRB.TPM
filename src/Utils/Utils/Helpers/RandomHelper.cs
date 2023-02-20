using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CRB.TPM.Utils.Helpers;

/// <summary>
/// 随机数辅助类
/// </summary>
//[SingletonInject]
public static class RandomHelper
{
    static int seed = Environment.TickCount;

    static readonly ThreadLocal<Random> random =
        new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

    public static int Next()
    {
        return random.Value.Next();
    }
    public static int Next(int max)
    {
        return random.Value.Next(max);
    }
    public static int Next(int min, int max)
    {
        return random.Value.Next(min, max);
    }
}
