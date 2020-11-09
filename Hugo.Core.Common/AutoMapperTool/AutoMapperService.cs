using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hugo.Core.Common.AutoMapperTool
{
    /// <summary>
    /// AutoMapper映射服务注入扩展
    /// <para>NuGet：Install-Package AutoMapper</para>
    /// <para>NuGet：Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection</para>
    /// </summary>
    public static class AutoMapperService
    {
        /// <summary>
        /// AutoMapper映射服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 映射指定程序集 - 解耦
            var basePath = AppContext.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Hugo.Core.AutoMapperProfile.dll");
            if (!File.Exists(fullPath))
            {
                var message = "未找到解耦项目文件 Hugo.Core.AutoMapperProfile.dll，请重新编译后再次运行，或检查并拷贝该文件至运行目录。";
                throw new Exception(message);
            }
            var assembly = Assembly.LoadFrom(fullPath);

            services.AddAutoMapper(MapType(assembly));
        }

        /// <summary>
        /// 通过反射自动化注册（继承自IProfile）的映射配置
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        private static Type[] MapType(Assembly assembly)
        {
            Type[] types = assembly.GetTypes().Where(predicate => predicate.IsSubclassOf(typeof(Profile))).ToArray();
            return types;
        }



        //public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        //{
        //    service.TryAddSingleton<MapperConfigurationExpression>();
        //    service.TryAddSingleton(serviceProvider =>
        //    {
        //        var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
        //        var instance = new MapperConfiguration(mapperConfigurationExpression);

        //        instance.AssertConfigurationIsValid();

        //        return instance;
        //    });
        //    service.TryAddSingleton(serviceProvider =>
        //    {
        //        var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

        //        return mapperConfiguration.CreateMapper();
        //    });

        //    return service;
        //}

        //public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        //{
        //    return applicationBuilder.ApplicationServices.GetRequiredService<MapperConfigurationExpression>();
        //}

    }

}
