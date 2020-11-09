using System;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Hugo.Core.Common.ORM
{
    /// <summary>
    /// SqlSugar服务注入扩展
    /// <para>NuGet：Install-Package SqlSugarCore</para>
    /// </summary>
    public static class SqlSugarService
    {
        /// <summary>
        /// SqlSugar服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <returns></returns>
        public static void AddSqlSugarClient(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // SqlSugar连接配置初始化
            var connectionConfig = new SqlSugar.ConnectionConfig()
            {
                ConnectionString = AppSettings.GetSetting("DataBase", "SQLServer", "ConnectionString"),//数据库连接字符串（必填）
                DbType = SqlSugar.DbType.SqlServer,//数据库类型（必填）
                IsAutoCloseConnection = true,//自动释放数据库连接（默认false）设置为true无需使用using或者Close操作
                InitKeyType = SqlSugar.InitKeyType.Attribute,//字段信息读取（默认SystemTable） 如：该属性是否主键、标识列等信息（//Attribute用于DbFirst  从数据库生成model的，//SystemTable用于Codefirst 从model库生成数据库表的）
                IsShardSameThread = false,//跨方法事务（默认false）设为true相同线程是同一个SqlConnection（开启后AOP失效）

                // 配置外部服务
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    DataInfoCacheService = new SqlSugarRedisCache(),//数据信息缓存服务
                },
                // 其他配置
                MoreSettings = new ConnMoreSettings
                {
                    //IsWithNoLockQuery = true,//是否无锁查询
                    IsAutoRemoveDataCache = true,//是否自动删除数据缓存
                    DefaultCacheDurationInSeconds = 0,//默认缓存持续时间（单位：秒）
                }
                // 从库
                //SlaveConnectionConfigs = listConfig_Slave,

                //// AOP事件
                //AopEvents = new AopEvents
                //{
                //    OnLogExecuting = (sql, p) =>
                //    {
                //        if (Appsettings.app(new string[] { "AppSettings", "SqlAOP", "Enabled" }).ObjToBool())
                //        {
                //            Parallel.For(0, 1, e =>
                //            {
                //                MiniProfiler.Current.CustomTiming("SQL：", GetParas(p) + "【SQL语句】：" + sql);
                //                LogLock.OutSql2Log("SqlLog", new string[] { GetParas(p), "【SQL语句】：" + sql });

                //            });
                //        }
                //    }
                //}
            };

            // SqlSugar连接配置注入
            services.AddSingleton(connectionConfig);

            // SqlSugar上下文工厂注入
            services.AddScoped<ISqlSugarFactory, SqlSugarFactory>();

            // SqlSugar实例注入（SqlSugarClient）
            services.AddScoped<ISqlSugarClient>(factory => new SqlSugar.SqlSugarClient(connectionConfig));

        }

        private static ConnectionConfig ConnectionConfigFactory(IServiceProvider applicationServiceProvider, Action<IServiceProvider, ConnectionConfig> configAction)
        {
            var config = new ConnectionConfig();
            configAction.Invoke(applicationServiceProvider, config);
            return config;
        }

    }
}