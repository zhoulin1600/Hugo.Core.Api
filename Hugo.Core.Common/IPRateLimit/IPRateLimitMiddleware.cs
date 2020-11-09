using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.IPRateLimit
{
    /// <summary>
    /// IP限流服务中间件（重写，app.UseIpRateLimiting()）
    /// <para>通常在项目中，Authorization授权是少不了了，加入限流后，在被限流的接口调用后，限流拦截器使得跨域策略失效，故重写拦截器中间件，继承IpRateLimitMiddleware</para>
    /// </summary>
    public class IPRateLimitMiddleware : IpRateLimitMiddleware
    {
        public IPRateLimitMiddleware(RequestDelegate next, IOptions<IpRateLimitOptions> options, IRateLimitCounterStore counterStore, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger)
            : base(next, options, counterStore, policyStore, config, logger)
        { }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
        }
    }

    /// <summary>
    /// IP限流服务中间件扩展类
    /// </summary>
    public static class IPRateLimitMiddlewareExtension
    {
        /// <summary>
        /// 启用IP限流服务中间件
        /// </summary>
        /// <param name="app">应用构造器</param>
        public static void UseIPRateLimitMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            if (AppSettings.GetSetting("Middleware", "IPRateLimit", "Enabled").ToBool())
            {
                app.UseMiddleware<IPRateLimitMiddleware>();//app.UseIpRateLimiting();
            }
        }
    }

}
