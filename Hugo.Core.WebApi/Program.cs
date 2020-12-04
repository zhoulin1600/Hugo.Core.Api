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
                // ʹ��Autofac�����ṩ����
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.AddFilter("System", LogLevel.Error); //����ϵͳĬ�ϵ�һЩ��־
                    loggingBuilder.AddFilter("Microsoft", LogLevel.Error);//����ϵͳĬ�ϵ�һЩ��־
                    loggingBuilder.AddLog4Net(Path.Combine(AppContext.BaseDirectory, "Logger/log4net.config"));//�����ļ�
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    // ����Զ��������ļ���IP�����������ã�
                    config.AddJsonFile($"appratelimit.json", optional: true, reloadOnChange: true);
                    // ����Զ��������ļ���Consulע��������ã�
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
