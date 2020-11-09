using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 响应结果格式化特性
    /// </summary>
    public class ResponseFormatAttribute : BaseActionFilterAsync
    {
        /// <summary>
        /// 重写 - 在执行操作Action方法后执行调用 - 响应结果格式化
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        /// <returns></returns>
        public override async Task OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ContainsFilter<ResponseNonFormatAttribute>())
                return;

            // 是否响应空结果
            if (context.Result is EmptyResult)
                context.Result = Success();
            else if (context.Result is ObjectResult res)
            {
                if (res.Value is ApiResult)
                    context.Result = JsonContent(res.Value.ToJson());
                else
                    context.Result = Success(res.Value);
            }

            await Task.CompletedTask;
        }
    }

}
