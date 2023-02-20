using System;


namespace CRB.TPM;

public static class GuidExtensions
{
    /// <summary>
    /// 判断Guid是否为空
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsEmpty(this Guid s)
    {
        return s == Guid.Empty;
    }

    /// <summary>
    /// 判断Guid是否不为空
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool NotEmpty(this Guid s)
    {
        return s != Guid.Empty;
    }

    /// <summary>
    /// 判断Guid是否不为空
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool NotNullAndEmpty(this Guid? s)
    {
        return s != null && s != Guid.Empty;
    }

    /// <summary>
    /// 判断Guid是否不为空
    /// </summary>
    /// <param name="s"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static bool NotEmptyIf(this Guid s, Action action)
    {
        var res = s != Guid.Empty;
        if(res) action.Invoke();
        return res;
    }

    /// <summary>
    /// 判断Guid是否不为空
    /// </summary>
    /// <param name="s"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static bool NotEmptyAndNullIf(this Guid? s, Action action)
    {
        var res = s.HasValue && s != Guid.Empty;
        if (res) action.Invoke();
        return res;
    }
}