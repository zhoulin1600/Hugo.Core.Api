using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Middleware
{
    /// <summary>
    /// 请求响应记录中间件
    /// </summary>
    public class ReuestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ReuestResponseLogMiddleware(RequestDelegate next, ILogger<ReuestResponseLogMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (AppSettings.GetSetting("Middleware", "RequestResponseLog", "Enabled").ToBool())
            {
                // 过滤，只有接口
                if (context.Request.Path.Value.Contains("api"))
                {
                    context.Request.EnableBuffering();
                    Stream originalBody = context.Response.Body;

                    try
                    {
                        // 存储请求数据
                        await RequestDataLog(context);

                        using (var ms = new MemoryStream())
                        {
                            context.Response.Body = ms;

                            await _next(context);

                            // 存储响应数据
                            ResponseDataLog(context.Response, ms);

                            ms.Position = 0;
                            await ms.CopyToAsync(originalBody);
                        }
                    }
                    catch (Exception)
                    {
                        // 记录异常
                        //ErrorLogData(context.Response, ex);
                    }
                    finally
                    {
                        context.Response.Body = originalBody;
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 请求信息记录
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        private async Task RequestDataLog(HttpContext context)
        {
            var request = context.Request;
            var sr = new StreamReader(request.Body);

            var message = $"\r\n QueryData：{request.Path + request.QueryString}\r\n BodyData：{await sr.ReadToEndAsync()}";

            if (!string.IsNullOrEmpty(message))
            {
                _logger.LogInformation(message);
                //Parallel.For(0, 1, e =>
                //{
                //    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Request Data:", content });

                //});
                request.Body.Position = 0;
            }
        }

        /// <summary>
        /// 响应信息记录
        /// </summary>
        /// <param name="response">Http响应</param>
        /// <param name="ms"></param>
        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var ResponseBody = new StreamReader(ms).ReadToEnd();

            // 去除 Html
            var reg = "<[^>]+>";
            var isHtml = Regex.IsMatch(ResponseBody, reg);

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                _logger.LogInformation($"ResponseData：{ResponseBody}");
                //Parallel.For(0, 1, e =>
                //{
                //    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });

                //});
            }
        }

    }

    /// <summary>
    /// 请求响应记录中间件扩展类
    /// </summary>
    public static class ReuestResponseMiddlewareExtension
    {
        /// <summary>
        /// 启用请求响应记录中间件
        /// </summary>
        /// <param name="app">应用构造器</param>
        /// <returns></returns>
        public static void UseReuestResponseMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ReuestResponseLogMiddleware>();
        }
    }
}
