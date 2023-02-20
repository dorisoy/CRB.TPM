using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 表示对象ID（ObjectId）
    /// objectId 比雪花算法id更适合微服务体系，不用考虑机器码分配问题，可部署机器更多，同时满足趋势递增的要求
    /// </summary>
    internal struct ObjectId : IComparable<ObjectId>, IEquatable<ObjectId>, IConvertible
    {

        private static readonly ObjectId __emptyInstance = default(ObjectId);
        private static readonly long __random = CalculateRandomValue();
        private static int __staticIncrement = (new Random()).Next();

        private readonly int _a;
        private readonly int _b;
        private readonly int _c;

        /// <summary>
        /// 初始化ObjectId类的新实例
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ObjectId(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length != 12)
            {
                throw new ArgumentException("字节数组必须为12字节长", "bytes");
            }

            FromByteArray(bytes, 0, out _a, out _b, out _c);
        }

        /// <summary>
        /// 初始化ObjectId类的新实例
        /// </summary>
        /// <param name="bytes">bytes.</param>
        /// <param name="index">ObjectId开始的字节数组的索引.</param>
        internal ObjectId(byte[] bytes, int index)
        {
            FromByteArray(bytes, index, out _a, out _b, out _c);
        }


        /// <summary>
        /// 初始化ObjectId类的新实例
        /// </summary>
        /// <param name="value">The value.</param>
        public ObjectId(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var bytes = BsonUtils.ParseHexString(value);
            FromByteArray(bytes, 0, out _a, out _b, out _c);
        }

        private ObjectId(int a, int b, int c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        /// <summary>
        /// 获取值为空的ObjectId的实例
        /// </summary>
        public static ObjectId Empty
        {
            get { return __emptyInstance; }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        public int Timestamp
        {
            get { return _a; }
        }

        /// <summary>
        /// 获取创建时间（从时间戳派生）
        /// </summary>
        public DateTime CreationTime
        {
            get { return BsonConstants.UnixEpoch.AddSeconds((uint)Timestamp); }
        }


        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns>如果第一个ObjectId小于第二个ObjectId，则为True.</returns>
        public static bool operator <(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns>小于等于.</returns>
        public static bool operator <=(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns>等于.</returns>
        public static bool operator ==(ObjectId lhs, ObjectId rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns>不等于.</returns>
        public static bool operator !=(ObjectId lhs, ObjectId rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns></returns>
        public static bool operator >=(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        /// <summary>
        /// 操作符重载，比较两个ObjectId
        /// </summary>
        /// <param name="lhs">第一个ObjectId.</param>
        /// <param name="rhs">=另一个ObjectId</param>
        /// <returns></returns>
        public static bool operator >(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }


        /// <summary>
        /// 生成具有唯一值的新ObjectId
        /// </summary>
        /// <returns></returns>
        public static ObjectId GenerateNewId()
        {
            return GenerateNewId(GetTimestampFromDateTime(DateTime.UtcNow));
        }

        public static string GenerateNewIdAsString()
        {
            return GenerateNewId().ToString();
        }

        /// <summary>
        /// 生成具有唯一值的新ObjectId（时间戳组件基于给定的DateTime）
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static ObjectId GenerateNewId(DateTime timestamp)
        {
            return GenerateNewId(GetTimestampFromDateTime(timestamp));
        }

        /// <summary>
        /// 生成具有唯一值（具有给定时间戳）的新ObjectId
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static ObjectId GenerateNewId(int timestamp)
        {
            // 仅使用低位3字节
            int increment = Interlocked.Increment(ref __staticIncrement) & 0x00ffffff; 
            return Create(timestamp, __random, increment);
        }

        /// <summary>
        /// 分析字符串并创建新的ObjectId
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static ObjectId Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            ObjectId objectId;
            if (TryParse(s, out objectId))
            {
                return objectId;
            }
            else
            {
                var message = string.Format("'{0}' 不是有效的24位十六进制字符串.", s);
                throw new FormatException(message);
            }
        }

        /// <summary>
        /// 尝试分析字符串并创建新的ObjectId
        /// </summary>
        /// <param name="s"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public static bool TryParse(string s, out ObjectId objectId)
        {
            // 如果s为null，则不抛出ArgumentNullException
            if (s != null && s.Length == 24)
            {
                byte[] bytes;
                if (BsonUtils.TryParseHexString(s, out bytes))
                {
                    objectId = new ObjectId(bytes);
                    return true;
                }
            }

            objectId = default(ObjectId);
            return false;
        }

        /// <summary>
        /// 计算随机值
        /// </summary>
        /// <returns></returns>
        private static long CalculateRandomValue()
        {
            var seed = (int)DateTime.UtcNow.Ticks ^ GetMachineHash() ^ GetPid();
            var random = new Random(seed);
            var high = random.Next();
            var low = random.Next();
            var combined = (long)((ulong)(uint)high << 32 | (ulong)(uint)low);
            //低位5字节
            return combined & 0xffffffffff; 
        }

        private static ObjectId Create(int timestamp, long random, int increment)
        {
            if (random < 0 || random > 0xffffffffff)
            {
                throw new ArgumentOutOfRangeException(nameof(random), "The random value must be between 0 and 1099511627775 (it must fit in 5 bytes).");
            }
            if (increment < 0 || increment > 0xffffff)
            {
                throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");
            }

            var a = timestamp;
            var b = (int)(random >> 8); // first 4 bytes of random
            var c = (int)(random << 24) | increment; // 5th byte of random and 3 byte increment
            return new ObjectId(a, b, c);
        }

        /// <summary>
        /// 获取当前进程id,此方法的存在是因为CAS在调用堆栈上的操作方式，即在执行该方法之前检查权限。因此，如果我们内联这个调用，调用方法将不会执行
        /// 在抛出一个异常之前，该异常需要在更高的级别上进行try/catch，而我们不必控制该级别.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int GetCurrentProcessId()
        {
            return Process.GetCurrentProcess().Id;
        }

        private static int GetMachineHash()
        {
            // 使用Dns.HostName，以便脱机工作
            var machineName = GetMachineName();
            //使用哈希的前3个字节
            return 0x00ffffff & machineName.GetHashCode(); 
        }

        private static string GetMachineName()
        {
            return Environment.MachineName;
        }

        private static short GetPid()
        {
            try
            {
                //仅使用低位两个字节
                return (short)GetCurrentProcessId(); 
            }
            catch (SecurityException)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static int GetTimestampFromDateTime(DateTime timestamp)
        {
            var secondsSinceEpoch = (long)Math.Floor((BsonUtils.ToUniversalTime(timestamp) - BsonConstants.UnixEpoch).TotalSeconds);
            if (secondsSinceEpoch < uint.MinValue || secondsSinceEpoch > uint.MaxValue)
            {
                throw new ArgumentOutOfRangeException("timestamp");
            }
            return (int)(uint)secondsSinceEpoch;
        }

        /// <summary>
        /// 从字节转化
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        private static void FromByteArray(byte[] bytes, int offset, out int a, out int b, out int c)
        {
            a = (bytes[offset] << 24) | (bytes[offset + 1] << 16) | (bytes[offset + 2] << 8) | bytes[offset + 3];
            b = (bytes[offset + 4] << 24) | (bytes[offset + 5] << 16) | (bytes[offset + 6] << 8) | bytes[offset + 7];
            c = (bytes[offset + 8] << 24) | (bytes[offset + 9] << 16) | (bytes[offset + 10] << 8) | bytes[offset + 11];
        }

        /// <summary>
        /// 将此ObjectId与其他ObjectId进行比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns>一个32位有符号整数，指示此ObjectId是否小于、等于或大于另一个ObjectId.</returns>
        public int CompareTo(ObjectId other)
        {
            int result = ((uint)_a).CompareTo((uint)other._a);
            if (result != 0) { return result; }
            result = ((uint)_b).CompareTo((uint)other._b);
            if (result != 0) { return result; }
            return ((uint)_c).CompareTo((uint)other._c);
        }

        /// <summary>
        /// 将此ObjectId与其他ObjectId进行比较
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns>是否相等</returns>
        public bool Equals(ObjectId rhs)
        {
            return
                _a == rhs._a &&
                _b == rhs._b &&
                _c == rhs._c;
        }

        /// <summary>
        /// 将此ObjectId与其他ObjectId进行比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果另一个对象是ObjectId并且等于此对象，则为True.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ObjectId)
            {
                return Equals((ObjectId)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = 37 * hash + _a.GetHashCode();
            hash = 37 * hash + _b.GetHashCode();
            hash = 37 * hash + _c.GetHashCode();
            return hash;
        }

        /// <summary>
        /// 转化字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            var bytes = new byte[12];
            ToByteArray(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// 将ObjectId转换为字节数组
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="offset">偏移量</param>
        public void ToByteArray(byte[] destination, int offset)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (offset + 12 > destination.Length)
            {
                throw new ArgumentException("Not enough room in destination buffer.", "offset");
            }

            destination[offset + 0] = (byte)(_a >> 24);
            destination[offset + 1] = (byte)(_a >> 16);
            destination[offset + 2] = (byte)(_a >> 8);
            destination[offset + 3] = (byte)(_a);
            destination[offset + 4] = (byte)(_b >> 24);
            destination[offset + 5] = (byte)(_b >> 16);
            destination[offset + 6] = (byte)(_b >> 8);
            destination[offset + 7] = (byte)(_b);
            destination[offset + 8] = (byte)(_c >> 24);
            destination[offset + 9] = (byte)(_c >> 16);
            destination[offset + 10] = (byte)(_c >> 8);
            destination[offset + 11] = (byte)(_c);
        }

        /// <summary>
        /// 返回值的字符串表示形式
        /// </summary>
        /// <returns>值的字符串表示形式</returns>
        public override string ToString()
        {
            var c = new char[24];
            c[0] = BsonUtils.ToHexChar((_a >> 28) & 0x0f);
            c[1] = BsonUtils.ToHexChar((_a >> 24) & 0x0f);
            c[2] = BsonUtils.ToHexChar((_a >> 20) & 0x0f);
            c[3] = BsonUtils.ToHexChar((_a >> 16) & 0x0f);
            c[4] = BsonUtils.ToHexChar((_a >> 12) & 0x0f);
            c[5] = BsonUtils.ToHexChar((_a >> 8) & 0x0f);
            c[6] = BsonUtils.ToHexChar((_a >> 4) & 0x0f);
            c[7] = BsonUtils.ToHexChar(_a & 0x0f);
            c[8] = BsonUtils.ToHexChar((_b >> 28) & 0x0f);
            c[9] = BsonUtils.ToHexChar((_b >> 24) & 0x0f);
            c[10] = BsonUtils.ToHexChar((_b >> 20) & 0x0f);
            c[11] = BsonUtils.ToHexChar((_b >> 16) & 0x0f);
            c[12] = BsonUtils.ToHexChar((_b >> 12) & 0x0f);
            c[13] = BsonUtils.ToHexChar((_b >> 8) & 0x0f);
            c[14] = BsonUtils.ToHexChar((_b >> 4) & 0x0f);
            c[15] = BsonUtils.ToHexChar(_b & 0x0f);
            c[16] = BsonUtils.ToHexChar((_c >> 28) & 0x0f);
            c[17] = BsonUtils.ToHexChar((_c >> 24) & 0x0f);
            c[18] = BsonUtils.ToHexChar((_c >> 20) & 0x0f);
            c[19] = BsonUtils.ToHexChar((_c >> 16) & 0x0f);
            c[20] = BsonUtils.ToHexChar((_c >> 12) & 0x0f);
            c[21] = BsonUtils.ToHexChar((_c >> 8) & 0x0f);
            c[22] = BsonUtils.ToHexChar((_c >> 4) & 0x0f);
            c[23] = BsonUtils.ToHexChar(_c & 0x0f);
            return new string(c);
        }

        /// <summary>
        /// 显式IConvertable实现
        /// </summary>
        /// <returns></returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            switch (Type.GetTypeCode(conversionType))
            {
                case TypeCode.String:
                    return ((IConvertible)this).ToString(provider);
                case TypeCode.Object:
                    if (conversionType == typeof(object) || conversionType == typeof(ObjectId))
                    {
                        return this;
                    }
                    break;
            }

            throw new InvalidCastException();
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }
    }

    #region BsonUtils

    /// <summary>
    /// 用于表示包含BSON实用程序方法的静态类
    /// </summary>
    internal static class BsonUtils
    {

        /// <summary>
        /// 获取适合在错误消息中使用的友好类名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFriendlyTypeName(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsGenericType)
            {
                return type.Name;
            }

            var sb = new StringBuilder();
            sb.AppendFormat("{0}<", Regex.Replace(type.Name, @"\`\d+$", ""));
            foreach (var typeParameter in type.GetTypeInfo().GetGenericArguments())
            {
                sb.AppendFormat("{0}, ", GetFriendlyTypeName(typeParameter));
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(">");
            return sb.ToString();
        }

        /// <summary>
        /// 将十六进制字符串解析为等效的字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static byte[] ParseHexString(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            byte[] bytes;
            if (!TryParseHexString(s, out bytes))
            {
                throw new FormatException("字符串应仅包含十六进制数字.");
            }

            return bytes;
        }

        /// <summary>
        /// 从Unix epoch之后的毫秒数转换为DateTime
        /// </summary>
        /// <param name="millisecondsSinceEpoch"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static DateTime ToDateTimeFromMillisecondsSinceEpoch(long millisecondsSinceEpoch)
        {
            if (millisecondsSinceEpoch < BsonConstants.DateTimeMinValueMillisecondsSinceEpoch ||
                millisecondsSinceEpoch > BsonConstants.DateTimeMaxValueMillisecondsSinceEpoch)
            {
                var message = string.Format("BsonDateTime毫秒SinceEpoch的值 {0} 在可转换为.NET DateTime的范围.",
                    millisecondsSinceEpoch);
                throw new ArgumentOutOfRangeException("millisecondsSinceEpoch", message);
            }

            // 必须特别处理MaxValue，以避免舍入错误
            if (millisecondsSinceEpoch == BsonConstants.DateTimeMaxValueMillisecondsSinceEpoch)
            {
                return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
            }
            else
            {
                return BsonConstants.UnixEpoch.AddTicks(millisecondsSinceEpoch * 10000);
            }
        }

        /// <summary>
        /// 将值转换为十六进制字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToHexChar(int value)
        {
            return (char)(value + (value < 10 ? '0' : 'a' - 10));
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            var length = bytes.Length;
            var c = new char[length * 2];

            for (int i = 0, j = 0; i < length; i++)
            {
                var b = bytes[i];
                c[j++] = ToHexChar(b >> 4);
                c[j++] = ToHexChar(b & 0x0f);
            }

            return new string(c);
        }

        /// <summary>
        /// 将DateTime转换为本地时间（对MinValue和MaxValue进行特殊处理）.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToLocalTime(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
            {
                return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Local);
            }
            else if (dateTime == DateTime.MaxValue)
            {
                return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Local);
            }
            else
            {
                return dateTime.ToLocalTime();
            }
        }

        /// <summary>
        /// 将DateTime转换为自Unix epoch以来的毫秒数.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToMillisecondsSinceEpoch(DateTime dateTime)
        {
            var utcDateTime = ToUniversalTime(dateTime);
            return (utcDateTime - BsonConstants.UnixEpoch).Ticks / 10000;
        }

        /// <summary>
        /// 将DateTime转换为UTC（对MinValue和MaxValue进行特殊处理）.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToUniversalTime(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
            {
                return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
            }
            else if (dateTime == DateTime.MaxValue)
            {
                return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
            }
            else
            {
                return dateTime.ToUniversalTime();
            }
        }

        /// <summary>
        /// 尝试将十六进制字符串解析为字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool TryParseHexString(string s, out byte[] bytes)
        {
            bytes = null;

            if (s == null)
            {
                return false;
            }

            var buffer = new byte[(s.Length + 1) / 2];

            var i = 0;
            var j = 0;

            if ((s.Length % 2) == 1)
            {
                // 如果s的长度为奇数，则假定隐含的前导“0”
                int y;
                if (!TryParseHexChar(s[i++], out y))
                {
                    return false;
                }
                buffer[j++] = (byte)y;
            }

            while (i < s.Length)
            {
                int x, y;
                if (!TryParseHexChar(s[i++], out x))
                {
                    return false;
                }
                if (!TryParseHexChar(s[i++], out y))
                {
                    return false;
                }
                buffer[j++] = (byte)((x << 4) | y);
            }

            bytes = buffer;
            return true;
        }


        private static bool TryParseHexChar(char c, out int value)
        {
            if (c >= '0' && c <= '9')
            {
                value = c - '0';
                return true;
            }

            if (c >= 'a' && c <= 'f')
            {
                value = 10 + (c - 'a');
                return true;
            }

            if (c >= 'A' && c <= 'F')
            {
                value = 10 + (c - 'A');
                return true;
            }

            value = 0;
            return false;
        }
    }

    #endregion

    #region BsonConstants

    /// <summary>
    /// 用于表示包含BSON常量的静态类
    /// </summary>
    internal static class BsonConstants
    {
        private static readonly long __dateTimeMaxValueMillisecondsSinceEpoch;
        private static readonly long __dateTimeMinValueMillisecondsSinceEpoch;
        private static readonly DateTime __unixEpoch;

        static BsonConstants()
        {
            // 必须先初始化unixEpoch
            __unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            __dateTimeMaxValueMillisecondsSinceEpoch = (DateTime.MaxValue - __unixEpoch).Ticks / 10000;
            __dateTimeMinValueMillisecondsSinceEpoch = (DateTime.MinValue - __unixEpoch).Ticks / 10000;
        }

        /// <summary>
        /// 获取DateTime.MaxValue的Unix历元之后的毫秒数
        /// </summary>
        public static long DateTimeMaxValueMillisecondsSinceEpoch
        {
            get { return __dateTimeMaxValueMillisecondsSinceEpoch; }
        }

        /// <summary>
        /// 获取DateTime.MinValue的Unix历元之后的毫秒数
        /// </summary>
        public static long DateTimeMinValueMillisecondsSinceEpoch
        {
            get { return __dateTimeMinValueMillisecondsSinceEpoch; }
        }

        /// <summary>
        /// 获取BSON DateTimes（1970-01-01）的Unix Epoch
        /// </summary>
        public static DateTime UnixEpoch { get { return __unixEpoch; } }
    }

    #endregion

}
