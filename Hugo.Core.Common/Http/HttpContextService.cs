using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Http
{
    /// <summary>
    /// HttpContext相关服务注入扩展
    /// </summary>
    public static class HttpContextService
    {
        /// <summary>
        /// HttpContext相关服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpContextService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 作用域注入HttpContext用户信息服务
            services.AddScoped<IHttpContextUser, HttpContextUser>();
        }
    }
}
