using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hugo.Core.Common.SignalR
{
    /// <summary>
    /// SignalR实时推送服务注入扩展
    /// <para>用于在网络上提供实时双向通信的组件</para>
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.SignalR</para>
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.SignalR.Protocols.NewtonsoftJson</para>
    /// </summary>
    public static class SignalRService
    {
        /// <summary>
        /// SignalR实时推送服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddSignalRService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSignalR().AddNewtonsoftJsonProtocol();//.AddJsonProtocol();

        }

    }
}
