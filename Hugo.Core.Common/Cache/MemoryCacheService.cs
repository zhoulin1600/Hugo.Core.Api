using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Cache
{
    /// <summary>
    /// Memory缓存服务注入扩展
    /// </summary>
    public static class MemoryCacheService
    {
        /// <summary>
        /// Memory缓存服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddMemoryCacheService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            
            //services.AddScoped<ICaching, MemoryCaching>();
            //services.AddSingleton<IMemoryCache>(factory =>
            //{
            //    var cache = new MemoryCache(new MemoryCacheOptions());
            //    return cache;
            //});
        }
    }
}
