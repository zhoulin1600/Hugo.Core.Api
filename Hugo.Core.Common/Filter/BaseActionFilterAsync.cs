using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 过滤器基类
    /// </summary>
    public abstract class BaseActionFilterAsync : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 在执行操作Action方法前执行调用
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        /// <returns></returns>
        public async virtual Task OnActionExecuting(ActionExecutingContext context)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 在执行操作Action方法后执行调用
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        /// <returns></returns>
        public async virtual Task OnActionExecuted(ActionExecutedContext context)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 模型绑定完成后，在操作之前异步调用
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        /// <param name="next">下一个动作过滤器或动作本身</param>
        /// <returns>一个System.Threading.Tasks.Task，完成时指示过滤器已执行</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await OnActionExecuting(context);
            if (context.Result == null)
            {
                var nextContext = await next();
                await OnActionExecuted(nextContext);
            }
        }

        #region 响应结果


        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public ContentResult JsonContent(string json)
        {
            return new ContentResult { Content = json, StatusCode = 200, ContentType = "application/json; charset=utf-8" };
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public ContentResult Success()
        {
            ApiResult res = new ApiResult
            {
                Success = true,
                Message = "请求成功"
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public ContentResult Success(string message)
        {
            ApiResult res = new ApiResult
            {
                Success = true,
                Message = message
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public ContentResult Success<T>(T data)
        {
            ApiResult<T> res = new ApiResult<T>
            {
                Success = true,
                Message = "请求成功",
                Data = data
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public ContentResult Error()
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = "请求失败"
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message">错误提示</param>
        /// <returns></returns>
        public ContentResult Error(string message)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = message
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message">错误提示</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        public ContentResult Error(string message, int errorCode)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = message,
                StatusCode = errorCode
            };

            return JsonContent(res.ToJson());
        }

        #endregion

    }
}
