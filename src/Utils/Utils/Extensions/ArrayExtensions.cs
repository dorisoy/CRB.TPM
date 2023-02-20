using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CRB.TPM;

/// <summary>
/// 数组扩展
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// 随机获取数组中的一个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static T RandomGet<T>(this T[] arr)
    {
        if (arr == null || !arr.Any())
            return default(T);

        var r = new Random();

        return arr[r.Next(arr.Length)];
    }
}
