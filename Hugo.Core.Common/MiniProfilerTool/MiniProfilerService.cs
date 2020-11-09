using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.MiniProfilerTool
{
    /// <summary>
    /// 性能分析器服务注入扩展
    /// <para>NuGet：Install-Package MiniProfiler.AspNetCore.Mvc</para>
    /// </summary>
    public static class MiniProfilerService
    {
        /// <summary>
        /// 性能分析器服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddMiniProfilerService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 添加MiniProfiler服务（3.x使用MiniProfiler，必须要注册MemoryCache服务）
            services.AddMiniProfiler(options =>
            {
                // 设定访问分析结果URL的路由基地址
                options.RouteBasePath = "/profiler";
                //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                options.PopupShowTimeWithChildren = true;

                // 可以增加权限
                //options.ResultsAuthorize = request => request.HttpContext.User.IsInRole("Admin");
                //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;
            });

        }
    }
}
