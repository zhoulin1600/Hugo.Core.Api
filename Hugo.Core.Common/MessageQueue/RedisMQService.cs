using InitQ;
using InitQ.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.MessageQueue
{
    /// <summary>
    /// Redis消息队列中间件服务注入扩展（消费者）
    /// <para>NuGet：Install-Package InitQ</para>
    /// </summary>
    public static class RedisMQService
    {
        /// <summary>
        /// Redis消息队列中间件服务注入（消费者）
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <param name="redisSubscribe">列表订阅</param>
        public static void AddRedisMQService(this IServiceCollection services, IRedisSubscribe redisSubscribe)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (AppSettings.GetSetting(new string[] { "Redis", "MQEnabled" }).ToBool())
            {
                // 队列消费者注入
                services.AddInitQ(setupAction =>
                {
                    // Redis服务器地址
                    setupAction.ConnectionString = AppSettings.GetSetting("Redis", "ConnectionString");
                    // 暂停时间(1000) 空队列等待间隔时间
                    setupAction.SuspendTime = 5000;
                    // 间隔时间(0) 队列消费的间隔时间
                    setupAction.IntervalTime = 0;
                    // 显示日志(false)
                    setupAction.ShowLog = false;
                    // 列表订阅     对应的订阅者类，需要new一个实例对象，当然你也可以传参，比如日志对象
                    setupAction.ListSubscribe = new List<IRedisSubscribe>() { redisSubscribe };
                });
            }

        }
    }

    /// <summary>
    /// Redis队列Key
    /// </summary>
    public static class RedisMQKey
    {
        public const string MQTESTKEY = "MQTEST";
    }

}
