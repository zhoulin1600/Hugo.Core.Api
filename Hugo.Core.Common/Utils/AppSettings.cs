using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hugo.Core.Common
{
    /// <summary>
    /// appsettings.json 读取类
    /// <para>NuGet：Install-Package Microsoft.Extensions.Configuration</para>
    /// <para>NuGet：Install-Package Microsoft.Extensions.Configuration.Json</para>
    /// <para>NuGet：Install-Package Microsoft.Extensions.Configuration.Binder</para>
    /// </summary>
    public static class AppSettings
    {
        private static IConfiguration _configuration;
        private static object _lock = new object();

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        if (_configuration == null)
                        {
                            _configuration = new ConfigurationBuilder()
                                .SetBasePath(AppContext.BaseDirectory)
                                // 根据环境变量区分配置文件 - $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"
                                .AddJsonFile("appsettings.json")
                                // 直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                                //.Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                                .Build();
                        }
                    }
                }

                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string GetSetting(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        }

        /// <summary>
        /// 递归获取配置信息数组
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static List<T> GetSetting<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="nameOfCon">连接字符串名</param>
        /// <returns></returns>
        public static string GetConnectionString(string nameOfCon)
        {
            return Configuration.GetConnectionString(nameOfCon);
        }
    }
}