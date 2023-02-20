using System;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示一个工作机器
    /// </summary>
    internal class IdWorker
    {
        /// <summary>
        /// 基准时间（2020-09-01）
        /// </summary>
        public const long Twepoch = 1598889600000L;

        /// <summary>
        /// 机器标识位数
        /// </summary>
        const int WorkerIdBits = 5;

        /// <summary>
        /// 数据标志位数
        /// </summary>
        const int DatacenterIdBits = 5;

        /// <summary>
        /// 序列号识位数，增加 SeqBitLength 会让性能更高，但生成的 ID 也会更长
        /// 如果生成ID速度不超过5W/s，不用修改任何配置参数
        /// 如果生成ID速度超过5W/s，低于50W，推荐修改：SeqBitLength=10
        /// 如果生成ID速度超过50W/s，接近500W，推荐修改：SeqBitLength=12
        /// </summary>
        const int SequenceBits = 12;

        /// <summary>
        /// 机器ID最大值
        /// </summary>
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 数据标志ID最大值
        /// </summary>
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 序列号ID最大值
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        /// <summary>
        /// 机器ID偏左移12位
        /// </summary>
        private const int WorkerIdShift = SequenceBits;

        /// <summary>
        /// 数据ID偏左移17位
        /// </summary>
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        /// <summary>
        /// 时间毫秒左移22位
        /// </summary>
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        /// <summary>
        ///  工作机器ID (特别提示：如果一台服务器部署多个独立服务，需要为每个服务指定不同的 WorkerId)
        ///  WorkerId，机器码，最重要参数，无默认值，必须 全局唯一（或相同 DataCenterId 内唯一），
        ///  必须 程序设定，缺省条件（WorkerIdBitLength取默认值）时最大值63，
        ///  理论最大值 2^WorkerIdBitLength-1（不同实现语言可能会限定在 65535 或 32767，原理同 WorkerIdBitLength 规则）。
        ///  不同机器或不同应用实例 不能相同，你可通过应用程序配置该值，也可通过调用外部服务获取值。
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 数据中心ID（机房ID，默认0），请确保全局唯一
        /// </summary>
        public long DatacenterId { get; protected set; }

        /// <summary>
        /// 毫秒内序列
        /// </summary>
        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        /// <summary>
        /// 实例工作机器
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="datacenterId"></param>
        /// <param name="sequence"></param>
        /// <exception cref="ArgumentException"></exception>
        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            // 如果超出范围就抛出异常
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(string.Format("worker Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(string.Format("region Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxDatacenterId));
            }

            //先检验再赋值
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;
        }

        readonly object _lock = new Object();

        /// <summary>
        /// 下一个ID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));
                }

                //如果上次生成时间和当前时间相同,在同一毫秒内
                if (_lastTimestamp == timestamp)
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    _sequence = (_sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    _sequence = 0;//new Random().Next(10);
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        /// <summary>
        /// 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        protected virtual long TimeGen()
        {
            return TimeExtensions.CurrentTimeMillis();
        }
    }
}