using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 分页数据查询返回对象
    /// </summary>
    public class PageResult<T> where T : class, new()
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { set; get; } = 10;

        /// <summary>
        /// 页数量
        /// </summary>
        public int PageCount { get; set; } = 1;

        /// <summary>
        /// 页数据列表
        /// </summary>
        public List<T> PageData { get; set; }

        /// <summary>
        /// 数据总量
        /// </summary>
        public int TotalCount { get; set; } = 0;

    }
}
