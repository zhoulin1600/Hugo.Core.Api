using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 缓存数据操作特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : BaseActionFilterAsync
    {
        /// <summary>
        /// 缓存绝对过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 10;

        /// <summary>
        /// 自定义缓存key值
        /// </summary>
        public string CustomKey { get; set; }

    }
}