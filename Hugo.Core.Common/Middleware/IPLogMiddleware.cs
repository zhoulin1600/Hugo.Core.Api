using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Middleware
{
    /// <summary>
    /// IP请求数据记录中间件
    /// </summary>
    public class IPLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public IPLogMiddleware(RequestDelegate next, ILogger<IPLogMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (AppSettings.GetSetting("Middleware", "IPLog", "Enabled").ToBool())
            {
                // 过滤，只有接口
                if (context.Request.Path.Value.Contains("api"))
                {
                    context.Request.EnableBuffering();

                    try
                    {
                        // 存储请求数据
                        var request = context.Request;
                        var message = $"IP地址：{GetClientIP(context)}，请求地址：{request.Path.ToString().TrimEnd('/').ToLower()}";

                        if (!string.IsNullOrEmpty(message))
                        {
                            _logger.LogInformation(message);
                            //// 自定义log输出
                            //Parallel.For(0, 1, e =>
                            //{
                            //    LogLock.OutSql2Log("RequestIpInfoLog", new string[] { requestInfo + "," }, false);
                            //});

                            //// 这种方案也行，用的是Serilog
                            //SerilogServer.WriteLog("RequestIpInfoLog", new string[] { requestInfo + "," }, false);

                            request.Body.Position = 0;
                        }

                        await _next(context);
                    }
                    catch (Exception)
                    {
                        // 记录异常
                        //ErrorLogData(context.Response, ex);
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

        private string GetWeek()
        {
            string week = string.Empty;
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                default:
                    week = "N/A";
                    break;
            }
            return week;
        }

        public static string GetClientIP(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

    }

    /// <summary>
    /// 请求响应记录中间件扩展类
    /// </summary>
    public static class IPLogMiddlewareExtension
    {
        /// <summary>
        /// 启用请求响应记录中间件
        /// </summary>
        /// <param name="app">应用构造器</param>
        /// <returns></returns>
        public static void UseIPLogMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<IPLogMiddleware>();
        }
    }
}
