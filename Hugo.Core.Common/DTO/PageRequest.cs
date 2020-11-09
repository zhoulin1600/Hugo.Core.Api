using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; } = 10;

    }

    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageRequest<SearchModel> : PageRequest where SearchModel : class, new()
    {
        /// <summary>
        /// 会员字段（作为查询条件）
        /// </summary>
        public SearchModel Search { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? BeginDateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 排序类型（升序：ASC，降序：DESC）默认：降序
        /// </summary>
        public string SortType { get; set; } = "DESC";

    }

}