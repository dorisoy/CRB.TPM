using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM;

public static class EnumerableExtensions
{
    /// <summary>
    /// 去重
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (TSource element in source)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }

    /// <summary>
    /// 判断集合不为NULL且元素数不为0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }

    /// <summary>
    /// 判断集合不为NULL且元素数不为0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool NotNullAndEmpty<T>(this IEnumerable<T> source)
    {
        return source != null && source.Any();
    }

    /// <summary>
    /// 判断集合不为NULL且元素数不为0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool NotNullAndEmptyIf<T>(this IEnumerable<T> source, Action action)
    {
        var res = source != null && source.Any();
        if (res)
        {
            action();
        }
        return res;
    }


    /// <summary>
    /// 添加符合条件的多个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    public static void AddRangeIf<T>(this ICollection<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var obj in values)
        {
            if (predicate(obj))
            {
                @this.Add(obj);
            }
        }
    }

    /// <summary>
    /// 添加符合条件的多个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    public static void AddRangeIf<T>(this ConcurrentBag<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var obj in values)
        {
            if (predicate(obj))
            {
                @this.Add(obj);
            }
        }
    }

    /// <summary>
    /// 添加符合条件的多个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    public static void AddRangeIf<T>(this ConcurrentQueue<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var obj in values)
        {
            if (predicate(obj))
            {
                @this.Enqueue(obj);
            }
        }
    }

    /// <summary>
    /// 添加不重复的元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="values"></param>
    public static void AddRangeIfNotContains<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T obj in values)
        {
            if (!@this.Contains(obj))
            {
                @this.Add(obj);
            }
        }
    }

    /// <summary>
    /// 移除符合条件的元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="where"></param>
    public static void RemoveWhere<T>(this ICollection<T> @this, Func<T, bool> @where)
    {
        foreach (var obj in @this.Where(where).ToList())
        {
            @this.Remove(obj);
        }
    }

    /// <summary>
    /// 移除符合条件的元素，不影响原生list
    /// </summary>
    /// <param name="this"></param>
    /// <param name="action">移除空id后，如果集合不为null且有元素则调用回调</param>
    /// <returns></returns>
    public static IList<Guid> RemoveGuidEmptyIf(this IList<Guid> @this, Action<IList<Guid>> action)
    {
        var newList = @this.RemoveGuidEmpty();
        if (newList != null && newList.Any())
        {
            action(newList);
        }
        return newList;
    }

    /// <summary>
    /// 移除符合条件的元素，不影响原生list
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static IList<Guid> RemoveGuidEmpty(this IList<Guid> @this)
    {
        var newList = @this?.Where(w => w.NotEmpty()).ToList();
        return newList;
    }

    /// <summary>
    /// 在元素之后添加元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition">条件</param>
    /// <param name="value">值</param>
    public static void InsertAfter<T>(this IList<T> list, Func<T, bool> condition, T value)
    {
        foreach (var item in list.Select((item, index) => new
        {
            item,
            index
        }).Where(p => condition(p.item)).OrderByDescending(p => p.index))
        {
            if (item.index + 1 == list.Count)
            {
                list.Add(value);
            }
            else
            {
                list.Insert(item.index + 1, value);
            }
        }
    }

    /// <summary>
    /// 在元素之后添加元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index">索引位置</param>
    /// <param name="value">值</param>
    public static void InsertAfter<T>(this IList<T> list, int index, T value)
    {
        foreach (var item in list.Select((v, i) => new
        {
            Value = v,
            Index = i
        }).Where(p => p.Index == index).OrderByDescending(p => p.Index))
        {
            if (item.Index + 1 == list.Count)
            {
                list.Add(value);
            }
            else
            {
                list.Insert(item.Index + 1, value);
            }
        }
    }

    /// <summary>
    /// 转HashSet
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static HashSet<TResult> ToHashSet<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
    {
        var set = new HashSet<TResult>();
        set.UnionWith(source.Select(selector));
        return set;
    }

    /// <summary>
    /// 异步Select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector)
    {
        return Task.WhenAll(source.Select(selector));
    }

    /// <summary>
    /// 异步Select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, int, Task<TResult>> selector)
    {
        return Task.WhenAll(source.Select(selector));
    }

    /// <summary>
    /// 异步Select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="maxParallelCount">最大并行数</param>
    /// <returns></returns>
    public static async Task<List<TResult>> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector, int maxParallelCount)
    {
        var results = new List<TResult>();
        var tasks = new List<Task<TResult>>();
        foreach (var item in source)
        {
            var task = selector(item);
            tasks.Add(task);
            if (tasks.Count >= maxParallelCount)
            {
                results.AddRange(await Task.WhenAll(tasks));
                tasks.Clear();
            }
        }

        results.AddRange(await Task.WhenAll(tasks));
        return results;
    }

    /// <summary>
    /// 将集合声明为非null集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> AsNotNull<T>(this List<T> list)
    {
        return list ?? new List<T>();
    }

    /// <summary>
    /// 将集合声明为非null集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> list)
    {
        return list ?? new List<T>();
    }

    /// <summary>
    /// 满足条件时执行筛选条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="condition"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> where)
    {
        return condition ? source.Where(where) : source;
    }

    /// <summary>
    /// 满足条件时执行筛选条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="condition"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<bool> condition, Func<T, bool> where)
    {
        return condition() ? source.Where(where) : source;
    }

    #region sql简单条件拼接
    /// <summary>
    /// 构建AND条件拼接字符串
    /// </summary>
    /// <param name="andList"></param>
    /// <param name="isBraces"></param>
    /// <returns></returns>
    public static string AndBuildStr(this ICollection<string> andList, bool isBraces = false)
    {
        return AndOrJoin(andList, "AND", isBraces);
    }
    /// <summary>
    /// 构建OR条件拼接字符串
    /// </summary>
    /// <param name="andList"></param>
    /// <param name="isBraces"></param>
    /// <returns></returns>
    public static string OrBuildStr(this ICollection<string> andList, bool isBraces = false)
    {
        return AndOrJoin(andList, "OR", isBraces);
    }
    private static string AndOrJoin(ICollection<string> andList, string joinStr, bool isBraces = false)
    {
        var list = andList.Where(w => w.NotNull());
        string res = string.Join($" {joinStr} ", list.Select(s => $" {s} "));
        if (list.Count() > 1 && isBraces)
        {
            res = $"({res.Trim()})";
        }
        return res;
    }
    #endregion
}