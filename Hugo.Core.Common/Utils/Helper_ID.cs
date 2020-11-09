using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// ID生成帮助类
    /// </summary>
    public class Helper_ID
    {
        internal static IdWorker IdWorker { get; set; }

        internal static IdHelperBootstrapper IdHelperBootstrapper { get; set; }

        /// <summary>
        /// 当前WorkerId,范围:1~1023
        /// </summary>
        public static long WorkerId { get => IdWorker.WorkerId; }

        /// <summary>
        /// 获取String型雪花Id
        /// </summary>
        /// <returns></returns>
        public static string GetId()
        {
            return GetLongId().ToString();
        }

        /// <summary>
        /// 获取long型雪花Id
        /// </summary>
        /// <returns></returns>
        public static long GetLongId()
        {
            //if (!IdHelperBootstrapper.Available())
            //    throw new Exception("当前系统异常,无法生成Id,请检查相关配置");

            return IdWorker.NextId();
        }

        /// <summary>
        /// 获取雪花Id
        /// </summary>
        /// <returns></returns>
        public static SnowflakeId GetStructId()
        {
            return new SnowflakeId(GetLongId());
        }
    }

    /// <summary>
    /// https://github.com/ccollie/snowflake-net
    /// </summary>
    public class IdWorker
    {
        public const long Twepoch = 1288834974657L;

        const int WorkerIdBits = 10;
        const int DatacenterIdBits = 0;
        const int SequenceBits = 12;
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;


        public IdWorker(long workerId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = 0;
            _sequence = sequence;

            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }

            //if (datacenterId > MaxDatacenterId || datacenterId < 0)
            //{
            //    throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            //}

            //log.info(
            //    String.Format("worker starting. timestamp left shift {0}, datacenter id bits {1}, worker id bits {2}, sequence bits {3}, workerid {4}",
            //                  TimestampLeftShift, DatacenterIdBits, WorkerIdBits, SequenceBits, workerId)
            //    );	
        }

        public long WorkerId { get; protected set; }
        public long DatacenterId { get; protected set; }

        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        // def get_timestamp() = System.currentTimeMillis

        readonly object _lock = new Object();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    //exceptionCounter.incr(1);
                    //log.Error("clock is moving backwards.  Rejecting requests until %d.", _lastTimestamp);
                    throw new InvalidSystemClock(String.Format(
                        "Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }

        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return SystemTime.CurrentTimeMillis();
        }
    }

    public class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message) : base(message) { }
    }

    public static class SystemTime
    {
        public static Func<long> currentTimeFunc = InternalCurrentTimeMillis;

        public static long CurrentTimeMillis()
        {
            return currentTimeFunc();
        }

        public static IDisposable StubCurrentTime(Func<long> func)
        {
            currentTimeFunc = func;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        public static IDisposable StubCurrentTime(long millis)
        {
            currentTimeFunc = () => millis;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }

    public class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }


    /// <summary>
    /// 配置引导
    /// </summary>
    public class IdHelperBootstrapper
    {
        /// <summary>
        /// 机器Id
        /// </summary>
        /// <value>
        /// 机器Id
        /// </value>
        protected long _worderId { get; set; }

        /// <summary>
        /// 获取机器Id
        /// </summary>
        /// <returns></returns>
        protected virtual long GetWorkerId()
        {
            return _worderId;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        /// <returns></returns>
        public virtual bool Available()
        {
            return true;
        }

        /// <summary>
        /// 设置机器Id
        /// </summary>
        /// <param name="workderId">机器Id</param>
        /// <returns></returns>
        public IdHelperBootstrapper SetWorkderId(long workderId)
        {
            _worderId = workderId;

            return this;
        }

        /// <summary>
        /// 完成配置
        /// </summary>
        public void Boot()
        {
            Helper_ID.IdWorker = new IdWorker(GetWorkerId());
            Helper_ID.IdHelperBootstrapper = this;
        }
    }

    /// <summary>
    /// 雪花Id,全局唯一,性能高,取代GUID
    /// </summary>
    public struct SnowflakeId
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">long形式ID</param>
        public SnowflakeId(long id)
        {
            Id = id;
            var numBin = Convert.ToString(Id, 2).PadLeft(64, '0');
            long timestamp = Convert.ToInt64(new string(numBin.Copy(1, 41).ToArray()), 2) + IdWorker.Twepoch;
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
            Time = dateTime.ToLocalTime();
        }

        /// <summary>
        /// 获取long形式Id
        /// </summary>
        /// <value>
        /// long形式Id
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Id时间
        /// </summary>
        /// <value>
        /// Id时间
        /// </value>
        public DateTime Time { get; }

        /// <summary>
        /// 转为string形式Id
        /// </summary>
        /// <returns>
        /// string形式Id
        /// </returns>
        public override string ToString()
        {
            return Id.ToString();
        }
    }

}
