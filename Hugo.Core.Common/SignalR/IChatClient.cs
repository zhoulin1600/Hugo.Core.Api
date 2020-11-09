using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.SignalR
{
    /// <summary>
    /// SignalR客户端调用
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// SignalR客户端方法 - 接收信息
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task ReceiveMessage(object message);

        /// <summary>
        /// SignalR客户端方法 - 接收信息
        /// </summary>
        /// <param name="userId">接收用户ID</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task ReceiveMessage(string userId, string message);

        /// <summary>
        /// SignalR客户端方法 - 接收更新
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task ReceiveUpdate(object message);
    }
}
