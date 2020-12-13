using System;

namespace Hugo.Core.Common
{
    /// <summary>
    /// Guid 拓展类
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取GUID码的字符串（"N"）
        /// </summary>
        /// <returns></returns>
        public static string ToNString(this Guid guid)
        {
            return guid.ToString("N");
        }

        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name="guid"></param>  
        /// <returns></returns>  
        public static string To16String(this Guid guid)
        {
            long i = 1;
            foreach (byte b in guid.ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// 得到GUID码的长整形结构
        /// 后话：原来BitConverter.ToInt64方法，只取buffer从startIndex开始向后加7个字节的值。
        /// 也就是说，我们16字节的高8个字节被忽略掉了。GUID理想情况下，要2^128个数据才会出现冲突，
        /// 而转换后，把字节数减半，也就是2^64数据就会出现冲突。
        /// </summary>
        /// <returns></returns>
        public static long ToLongString(this Guid guid)
        {
            byte[] buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 转为有序的GUID
        /// 注：长度为50字符
        /// </summary>
        /// <param name="guid">新的GUID</param>
        /// <returns></returns>
        public static string ToSequentialGuid(this Guid guid)
        {
            var timeStr = (DateTime.Now.ToCstTime().Ticks / 10000).ToString("x8");
            var newGuid = $"{timeStr.PadLeft(13, '0')}-{guid}";

            return newGuid;
        }

        /// <summary>
        /// 转为主键且有序的GUID（注：长度为50字符）
        /// </summary>
        /// <param name="guid">新的GUID</param>
        /// <returns></returns>
        public static string ToPrimaryKey(this Guid guid)
        {
            //var timeStr = (DateTime.Now.ToCstTime().Ticks / 10000).ToString("x8");
            //var newGuid = $"{timeStr.PadLeft(13, '0')}-{guid}";

            //return newGuid.ToUpper();

            return $"{guid.ToLongString()}"; //{DateTime.Now.ToUnixTimeStamp()}-
        }
    }
}
