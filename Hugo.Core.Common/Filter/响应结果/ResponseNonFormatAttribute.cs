using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 响应结果非格式化特性
    /// </summary>
    public class ResponseNonFormatAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 重写 - 在执行操作Action方法前执行调用
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        /// <summary>
        /// 重写 - 在执行操作Action方法后执行调用
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
