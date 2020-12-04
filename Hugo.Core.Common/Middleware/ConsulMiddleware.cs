using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Middleware
{
    /// <summary>
    /// Consul注册服务中间件
    /// <para>NuGet：Install-Package Consul</para>
    /// </summary>
    public static class ConsulMiddleware
    {
        /// <summary>
        /// Consul注册服务中间件
        /// </summary>
        /// <param name="app">应用构造器</param>
        /// <param name="configuration">配置文件</param>
        /// <param name="lifetime"></param>
        public static void UseConsulMiddleware(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            if (configuration["Middleware:Consul:Enabled"].ToBool())
            {
                var consulClient = new ConsulClient(c =>
                {
                    c.Address = new Uri(configuration["ConsulSetting:ConsulAddress"]);//Consul服务器地址
                });

                string[] tags = configuration.GetSection("ConsulSetting:Tags").Get<string[]>();
                IDictionary<string, string> meta = configuration.GetSection("ConsulSetting:Meta:0").Get<Dictionary<string, string>>();
                // 初始化Consul服务注册实例
                var registration = new AgentServiceRegistration()
                {
                    ID = $"Service：{Guid.NewGuid()}",//服务实例唯一标识
                    Name = configuration["ConsulSetting:ServiceName"],//服务名（组名）
                    Address = configuration["ConsulSetting:ServiceIP"],//服务IP
                    Port = int.Parse(configuration["ConsulSetting:ServicePort"]),//服务端口
                    Tags = tags,//服务标签
                    Meta = meta,//服务参数字典
                    Check = new AgentServiceCheck()
                    {
                        // 服务健康检查
                        HTTP = $"http://{configuration["ConsulSetting:ServiceIP"]}:{configuration["ConsulSetting:ServicePort"]}{configuration["ConsulSetting:ServiceHealthCheck:HTTP"]}",//健康检查地址
                        Interval = TimeSpan.FromSeconds(configuration["ConsulSetting:ServiceHealthCheck:Interval"].ToInt()),//健康检查时间间隔
                        Timeout = TimeSpan.FromSeconds(configuration["ConsulSetting:ServiceHealthCheck:Timeout"].ToInt()),//健康检查超时时间
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(configuration["ConsulSetting:ServiceHealthCheck:DeregisterCriticalServiceAfter"].ToInt())//健康检查失败后多久移除（60秒）
                    }
                };

                // 服务注册
                consulClient.Agent.ServiceRegister(registration).Wait();

                // 应用程序终止时，取消注册
                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                });
            }

        }
    }
}
