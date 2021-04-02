using Hugo.Core.Common.MessageQueue;
using InitQ.Abstractions;
using InitQ.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.WebApi.MessageQueue
{
    /// <summary>
    /// Redis消息队列订阅者
    /// </summary>
    public class RedisSubscribe : IRedisSubscribe
    {
        /// <summary>
        /// 消费者1
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Subscribe(RedisMQKey.MQTESTKEY)]
        private async Task SubRedisTest1(string msg)
        {
            Console.WriteLine($"SubRedisTest1：队列{RedisMQKey.MQTESTKEY} 消费到/接受到 消息:{msg}");

            await Task.CompletedTask;
        }

        /// <summary>
        /// 消费者2
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Subscribe(RedisMQKey.MQTESTKEY)]
        private async Task SubRedisTest2(string msg)
        {
            Console.WriteLine($"SubRedisTest2：队列{RedisMQKey.MQTESTKEY} 消费到/接受到 消息:{msg}");

            await Task.CompletedTask;
        }
    }
}
