using Hugo.Core.Common.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Hugo.Core.Common.Controller
{
    /// <summary>
    /// 自定义路由模版
    /// <para>用于解决swagger文档No operations defined in spec!问题</para>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    //[Route("Api/[controller]/[action]")]
    [ResponseFormat]//响应格式化
    //[Authorize]//认证授权
    //[AllowAnonymous]//允许匿名
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 自定义请求头参数
        /// </summary>
        public string CustomPara => GetCustomPara();

        /// <summary>
        /// 获取请求头中的商户店铺ID
        /// </summary>
        /// <returns>商户店铺ID</returns>
        private string GetCustomPara()
        {
            string customPara = string.Empty;

            customPara = Request.Headers["CustomPara"].ToString();

            return customPara;
        }


        #region 响应结果


        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        protected ContentResult JsonContent(string jsonStr)
        {
            return base.Content(jsonStr, "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 返回html
        /// </summary>
        /// <param name="body">html内容</param>
        /// <returns></returns>
        protected ContentResult HtmlContent(string body)
        {
            return base.Content(body);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        protected ApiResult Success()
        {
            ApiResult res = new ApiResult
            {
                Success = true,
                Message = "请求成功",
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        protected ApiResult<T> Success<T>(T data)
        {
            ApiResult<T> res = new ApiResult<T>
            {
                Success = true,
                Message = "操作成功",
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        protected ApiResult<T> Success<T>(T data, string message)
        {
            ApiResult<T> res = new ApiResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        protected ApiResult Error()
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = "请求失败",
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message">错误提示</param>
        /// <returns></returns>
        protected ApiResult Error(string message)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = message,
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message">错误提示</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        protected ApiResult Error(string message, int errorCode)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = message,
                StatusCode = errorCode
            };

            return res;
        }

        #endregion

    }
}