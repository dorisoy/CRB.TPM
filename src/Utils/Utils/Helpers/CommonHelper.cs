using CRB.TPM.Utils.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Helpers;

public class CommonHelper
{
    #region Properties

    /// <summary>
    /// TPM默认文件操作提供器
    /// </summary>
    public static ITPMFileProvider DefaultFileProvider { get; set; }

    #endregion

    /// <summary>
    /// 数组中最大的值
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int GetMax(int[] array)
    {
        int max = 0;
        for (int i = 0; i < array.Length; i++)
        {
            max = max > array[i] ? max : array[i];
        }
        return max;
    }

    public static T To<T>(object value)
    {
        //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        return (T)To(value, typeof(T));
    }

    public static object To(object value, Type destinationType)
    {
        return To(value, destinationType, CultureInfo.InvariantCulture);
    }

    public static object To(object value, Type destinationType, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        var sourceType = value.GetType();

        var destinationConverter = TypeDescriptor.GetConverter(destinationType);
        if (destinationConverter.CanConvertFrom(value.GetType()))
        {
            return destinationConverter.ConvertFrom(null, culture, value);
        }

        var sourceConverter = TypeDescriptor.GetConverter(sourceType);
        if (sourceConverter.CanConvertTo(destinationType))
        {
            return sourceConverter.ConvertTo(null, culture, value, destinationType);
        }

        if (destinationType.IsEnum && value is int)
        {
            return Enum.ToObject(destinationType, (int)value);
        }

        if (!destinationType.IsInstanceOfType(value))
        {
            return Convert.ChangeType(value, destinationType, culture);
        }

        return value;
    }
}
