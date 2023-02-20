using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 公共基础提供
    /// </summary>
    internal class CommonUtil
    {
        /// <summary>
        /// 关键字处理[name] `name`
        /// 获取id,sex,name
        /// </summary>
        /// <param name="fieldList"></param>
        /// <param name="leftChar">左符号</param>
        /// <param name="rightChar">右符号</param>
        /// <returns></returns>
        public static string GetFieldsStr(IEnumerable<string> fieldList, string leftChar, string rightChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in fieldList)
            {
                sb.Append($"{leftChar}{item}{rightChar}");

                if (item != fieldList.Last())
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// //获取@id,@sex,@name
        /// </summary>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public static string GetFieldsAtStr(IEnumerable<string> fieldList, string symbol = "@", SqlFieldEntity entity = null) //oracle @换成 
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in fieldList)
            {
                if (entity != null && entity.OtherFieldDict.Count > 0)
                {
                    var ok = entity.OtherFieldDict.TryGetValue(item, out var dict);
                    if (ok)
                    {
                        if (dict == -10)
                        {
                            sb.Append($"{symbol}{item}::jsonb");
                        }
                        else if (dict == -11)
                        {
                            sb.Append($"{symbol}{item}::json");
                        }
                    }
                    else
                    {
                        sb.Append($"{symbol}{item}");
                    }
                }
                else
                {
                    sb.Append($"{symbol}{item}");
                }

                if (item != fieldList.Last())
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// 关键字处理[name] `name`
        /// 获取id=@id,name=@name
        /// </summary>
        /// <param name="fieldList"></param>
        /// <param name="leftChar">左符号</param>
        /// <param name="rightChar">右符号</param>
        /// <returns></returns>
        public static string GetFieldsAtEqStr(IEnumerable<string> fieldList, string leftChar, string rightChar, string symbol = "@", SqlFieldEntity entity = null) //oracle @换成 
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in fieldList)
            {

                if (entity != null && entity.OtherFieldDict.Count > 0)
                {
                    var ok = entity.OtherFieldDict.TryGetValue(item, out var dict);
                    if (ok)
                    {
                        if (dict == -10)
                        {
                            sb.Append($"{leftChar}{item}{rightChar}={symbol}{item}::jsonb");
                        }
                        else if (dict == -11)
                        {
                            sb.Append($"{leftChar}{item}{rightChar}={symbol}{item}::json");
                        }
                    }
                    else
                    {
                        sb.Append($"{leftChar}{item}{rightChar}={symbol}{item}");
                    }
                }
                else
                {
                    sb.Append($"{leftChar}{item}{rightChar}={symbol}{item}");
                }

                if (item != fieldList.Last())
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public static IEnumerable GetMultiExec(object param)
        {
            return (param is IEnumerable && !(param is string || param is IEnumerable<KeyValuePair<string, object>>)) ? (IEnumerable)param : null;
        }

        /// <summary>
        /// 判断输入参数是否有个数，用于in判断
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool ObjectIsEmpty(object param)
        {
            bool result = true;
            IEnumerable data = GetMultiExec(param);
            if (data != null)
            {
                foreach (var item in data)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    }
}
