using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Hugo.Core.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 使用Autofac服务提供工厂
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.AddFilter("System", LogLevel.Error); //过滤系统默认的一些日志
                    loggingBuilder.AddFilter("Microsoft", LogLevel.Error);//过滤系统默认的一些日志
                    loggingBuilder.AddLog4Net(Path.Combine(AppContext.BaseDirectory, "Logger/log4net.config"));//配置文件
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    // 添加自定义配置文件（IP限流服务配置）
                    config.AddJsonFile($"appratelimit.json", optional: true, reloadOnChange: true);
                    // 添加自定义配置文件（Consul注册服务配置）
                    config.AddJsonFile($"appconsul.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseUrls("http://*:9000")
                    .UseStartup<Startup>();
                });

    }
}
