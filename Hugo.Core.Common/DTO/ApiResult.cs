using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 状态码（默认：200-正常）
        /// </summary>
        public int StatusCode { get; set; } = 200;

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }

    }

    /// <summary>
    /// 响应结果
    /// </summary>
    /// <typeparam name="T">Data数据</typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
    }



}
