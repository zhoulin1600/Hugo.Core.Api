using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Cors
{
    /// <summary>
    /// Cors跨域服务注入扩展
    /// </summary>
    public static class CorsService
    {
        /// <summary>
        /// Cors跨域策略服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddCorsService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            /*在Startup中间件指派跨域请求方式*/
            services.AddCors(setupAction =>
            {
                if (AppSettings.GetSetting("Cors", "EnableAllDomain").ToBool())
                {
                    // 无限制策略（允许任意跨域请求）
                    setupAction.AddPolicy(AppSettings.GetSetting("Cors", "PolicyName"),
                        policy =>
                        {
                            policy
                            //.SetIsOriginAllowed(isOriginAllowed => true)
                            .AllowAnyOrigin()//允许任何来源 -- context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            .AllowAnyHeader()//允许任何标头 -- context.Response.Headers.Add("Access-Control-Allow-Headers", context.Request.Headers["Access-Control-Request-Headers"]);
                            .AllowAnyMethod();//允许任何方法 -- context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                        });
                }
                else
                {
                    // 限制请求域策略（允许指定域名跨域请求）
                    setupAction.AddPolicy(AppSettings.GetSetting("Cors", "PolicyName"),
                        policy =>
                        {
                            policy
                            .WithOrigins(AppSettings.GetSetting("Cors", "Origins").Split(','))//将指定的来源加入策略
                            .AllowAnyHeader()//允许任何标头
                            .AllowAnyMethod()//允许任何方法
                            .AllowCredentials();//允许cookie凭据
                        });
                }
            });

        }

    }
}
