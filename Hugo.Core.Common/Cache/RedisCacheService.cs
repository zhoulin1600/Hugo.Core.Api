using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Cache
{
    /// <summary>
    /// CSRedisCore缓存服务注入扩展
    /// <para>NuGet：Install-Package CSRedisCore</para>
    /// <para>NuGet：Install-Package Caching.CSRedis</para>
    /// </summary>
    public static class RedisCacheService
    {
        /// <summary>
        /// CSRedisCore缓存服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddCSRedisCacheService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 普通模式 和 初始化RedisHelper
            var csredis = new CSRedis.CSRedisClient(AppSettings.GetSetting("Redis", "ConnectionString"));//Configuration["RedisCaching:ConnectionString"]
            RedisHelper.Initialization(csredis);
            services.AddSingleton(csredis);

            // 注册Redis分布式缓存 ↓web项目要注册↓  NuGet：Install-Package Caching.CSRedis
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));

            //services.AddSingleton<IDistributedCache>(new CSRedisCache(new CSRedis.CSRedisClient(Core.Common.Appsettings.app("RedisCaching", "ConnectionString"))));
            //services.AddScoped<ICSRedisHelper, CSRedisHelper>();



            // 使用Session（需要IDistributedCache支持）
            //services.AddSession();


        }
    }
}
