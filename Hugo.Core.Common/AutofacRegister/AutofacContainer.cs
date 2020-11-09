using Autofac;
using Autofac.Extras.DynamicProxy;
using Hugo.Core.Common.AOP;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Hugo.Core.Common.AutofacRegister
{
    /// <summary>
    /// Autofac服务容器（ IOC容器，DI依赖注入，AOP切面等 ）
    /// <para>NuGet：Install-Package Autofac.Extras.DynamicProxy（Autofac的动态代理，依赖Autofac）</para>
    /// <para>NuGet：Install-Package Autofac.Extensions.DependencyInjection（Autofac的依赖注入扩展）</para>
    /// </summary>
    public class AutofacContainer : Autofac.Module
    {        
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory; //Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region 注入有接口层的服务层（Load：'Hugo.Core.Service'加载给定程序集名称，需要引用该程序集）（LoadFrom：'D:\\Hugo\\Hugo.Core.Service.dll'加载给定程序集的完整路径文件名，无需引用该程序集，实现dll解耦）
            var servicesDllFile = Path.Combine(basePath, "Hugo.Core.Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "Hugo.Core.Repository.dll");
            if (!File.Exists(servicesDllFile) || !File.Exists(repositoryDllFile))
            {
                var message = "未找到解耦项目文件 Hugo.Core.Service.dll 或 Hugo.Core.Repository.dll，请重新编译后再次运行，或检查并拷贝该文件至运行目录。";
                throw new Exception(message);
            }

            // AOP切面拦截处理配置，注意叠加顺序，类似管道的工作流程，执行方法兜底
            var interceptorTypes = new List<Type>();
            // Redis缓存数据AOP拦截器
            if (AppSettings.GetSetting(new string[] { "Interceptor", "CacheRedisAOP", "Enabled" }).ToBool())
            {
                builder.RegisterType<CacheRedisAOP>();
                interceptorTypes.Add(typeof(CacheRedisAOP));
            }
            // Memory缓存数据AOP拦截器
            if (AppSettings.GetSetting(new string[] { "Interceptor", "CacheMemoryAOP", "Enabled" }).ToBool())
            {
                builder.RegisterType<CacheMemoryAOP>();
                interceptorTypes.Add(typeof(CacheMemoryAOP));
            }
            // SqlSugar数据库事务AOP拦截器
            if (AppSettings.GetSetting(new string[] { "Interceptor", "SqlSugarTransactionAOP", "Enabled" }).ToBool())
            {
                builder.RegisterType<SqlSugarTransactionAOP>();
                interceptorTypes.Add(typeof(SqlSugarTransactionAOP));
            }
            // Method方法日志AOP拦截器
            if (AppSettings.GetSetting(new string[] { "Interceptor", "MethodLogAOP", "Enabled" }).ToBool())
            {
                builder.RegisterType<MethodLogAOP>();
                interceptorTypes.Add(typeof(MethodLogAOP));
            }
            // 用户操作日志AOP拦截器
            if (AppSettings.GetSetting(new string[] { "Interceptor", "OperationLogAOP", "Enabled" }).ToBool())
            {
                builder.RegisterType<OperationLogAOP>();
                interceptorTypes.Add(typeof(OperationLogAOP));
            }

            // 注入Service
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);// Assembly.Load("Hugo.Core.Service");
            builder.RegisterAssemblyTypes(assemblysServices)
                //.Where(t => t.Name.EndsWith("Service") && !t.IsAbstract) //类名以Service结尾，且类型不能是抽象的
                .InstancePerDependency()//生命周期 .SingleInstance()全局单例 .InstancePerLifetimeScope()请求作用域 .InstancePerDependency()瞬时每次
                .AsImplementedInterfaces()//自动以其实现的所有接口类型暴露（包括IDisposable接口）
                .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy; //对目标类型启用接口拦截。拦截器将通过类或接口上的Intercept属性确定，或通过InterceptedBy()调用添加。
                .InterceptedBy(interceptorTypes.ToArray());//添加指定拦截器到注入容器的接口或类

            // 注入Repository
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);// Assembly.Load("Hugo.Core.Repository");
            builder.RegisterAssemblyTypes(assemblysRepository)
                .InstancePerDependency()//生命周期 .SingleInstance()全局单例 .InstancePerLifetimeScope()请求作用域 .InstancePerDependency()瞬时每次
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）
            #endregion

            #region 注入无接口层的服务层
            //// 因为没有接口层，所以不能实现解耦，只能用 Load 方法。
            //// 注意如果使用没有接口的服务，并想对其使用 AOP 拦截，就必须设置为虚方法
            //var assemblysServicesNoInterfaces = Assembly.Load("Hugo.Core.Service");
            //builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces)
            //    .InstancePerDependency();
            #endregion

            #region 注入有接口的类，启用interface代理拦截
            //builder.RegisterType<SysUserService>().As<ISysUserService>()
            //    .InstancePerDependency()
            //    .AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(typeof(LogAOP));
            #endregion

            #region 注入无接口的类，启用class代理拦截
            //// 只能注入该类中的虚方法，且必须是public
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(SystemClock)))
            //    .SingleInstance()
            //    .EnableClassInterceptors()
            //    .InterceptedBy(typeof(LogAOP));
            #endregion

        }
    }
}