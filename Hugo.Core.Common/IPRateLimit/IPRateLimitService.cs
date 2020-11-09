using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.IPRateLimit
{
    /// <summary>
    /// IP限流服务注入扩展
    /// <para>NuGet：Install-Package AspNetCoreRateLimit</para>
    /// </summary>
    public static class IPRateLimitService
    {
        /// <summary>
        /// IP限流服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <param name="configuration">配置信息</param>
        public static void AddIPRateLimitService(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 需要存储速率限制计数器和ip规则 needed to store rate limit counters and ip rules
            //services.AddMemoryCache();
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = "127.0.0.1:6379,password=123456,connectTimeout=5000,syncTimeout=10000";
            //    options.InstanceName = "WebRatelimit";
            //});
            
            // 加载配置
            services.AddOptions();
            // 从appsettings.json加载常规配置 load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            //// 注入计数器和规则存储 inject counter and rules stores
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // 注入计数器和规则分布式缓存存储 inject counter and rules distributed cache stores
            services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();

            // 使用clientId/clientIp解析器 the clientId/clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // 配置（解析器、计数器键生成器） configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        }
    }
}
