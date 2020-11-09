using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.SignalR
{
    /// <summary>
    /// SignalR集线器（继承 SignalR集线器的基类）
    /// <para>用于在网络上提供实时双向通信的组件</para>
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.SignalR</para>
    /// </summary>
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// 与集线器建立新连接时运行
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //TODO..
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 与集线器断开连接时运行
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(System.Exception ex)
        {
            //TODO..
            return base.OnDisconnectedAsync(ex);
        }



        /// <summary>
        /// 将连接添加到指定的组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task AddToGroupAsync(string groupName)
        {
            // Context.ConnectionId：客户端连接ID，要添加到组的连接ID
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 从指定的组中删除连接
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task RemoveFromGroupAsync(string groupName)
        {
            // Context.ConnectionId：客户端连接ID，要从组中删除的连接ID
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }



        /// <summary>
        /// 向指定群组发送信息
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="message">信息内容</param>  
        /// <returns></returns>
        public async Task SendMessageToGroupAsync(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessage(message);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task SendMessageToUserAsync(string userId, string message)
        {
            await Clients.User(userId).ReceiveMessage(message);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task SendMessage(string userId, string message)
        {
            await Clients.All.ReceiveMessage(userId, message);
        }


        /// <summary>
        /// 获取最新计数
        /// <para>定于一个通讯管道，用来管理我们和客户端的连接</para>
        /// 1、客户端调用 GetLatestCount，就像订阅
        /// </summary>
        /// <param name="random">随机字符串</param>
        /// <returns></returns>
        public async Task GetLatestCount(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            await Clients.All.ReceiveUpdate("客户端订阅");

            //3、客户端再通过 ReceiveUpdate ，来接收

        }

    }
}

/********* 一、Vue 中配置客户端连接
 

 1、安装库依赖包
 npm install @aspnet/signalr

 2、执行页面添加引用
 import * as signalR from "@aspnet/signalr"
 
 3、页面初始化时创建连接
 created: function () {
     // 实例化一个连接器
     this.connection = new signalR.HubConnectionBuilder()
         // 配置通道路由
         .withUrl('/api2/chatHub')
         // 自动重新连接
         .withAutomaticReconnect()
         // 日志信息
         .configureLogging(signalR.LogLevel.Information)
         // 创建
         .build();
 }
 
 4、客户端调用集线器——建立连接，呼叫对方
 // 开始通讯，并成功呼叫服务器
 thisvue.connection.start().then(() => {
     thisvue.connection.invoke('GetLatestCount', 1).catch(function (err) {
         return console.error(err);
     });
 });
 GetLatestCount：服务端Hub的方法名
 
 5、从集线器调用客户端方法——接收回应
 mounted() {
    thisVue.connection.on('ReceiveUpdate', function (update) {
        console.info('update success!')
        thisVue.tableData = update;//将返回的数据，实时的赋值给当前页面的 data 中；
    })
 },
 ReceiveUpdate：客户端定义的方法名

 接口请求：chatHub?id=xxxxx（这个时候我们刷新页面，已经能看到消息了，然后我们在看看接口请求）
 id：自动生成的用户userId（不随着消息推送而变化，只有每次请求重新连接的时候，才会变化）

 6、等待服务端的推送（如果想中断连接，只需要页面关闭的时候，执行 connection.stop() 即可）
 


********** 二、服务端数据信息推送到客户端——实时短信

 private readonly IHubContext<ChatHub> _hubContext;

 public GlobalExceptionsFilter(IHubContext<ChatHub> hubContext)
 {
     _hubContext = hubContext;
 }

 _hubContext.Clients.All.SendAsync("ReceiveUpdate", "推送的数据信息").Wait();


不同对象进行推送示例：

 /// <summary>
 /// 全员推送
 /// </summary>
 /// <returns></returns>
 [HttpPost]
 public async Task<IActionResult> PushMessageAsync([FromBody]object data)
 {
     await _hubContext.Clients.All.SendAsync(MessageDefault.ReceiveMessage, data);
     return Ok();
 }

 /// <summary>
 /// 对某人推送
 /// </summary>
 /// <returns></returns>
 [HttpPost]
 public async Task<IActionResult> PushAnyOneAsync([FromBody]MessagePushDTO model)
 {
     if (model == null)
     {
         return Forbid();
     }
     var user = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == model.UserId && m.GroupName == model.GroupName);
     if (user != null)
     {
         await _hubContext.Clients.Client(user.ConnectionId).SendAsync(MessageDefault.ReceiveAnyOne, model.MsgJson);
     }
     return Ok();
 }

 /// <summary>
 /// 对某组进行推送
 /// </summary>
 /// <returns></returns>
 [HttpPost]
 public async Task<IActionResult> PushGroupAsync([FromBody]MessageGroupPushDTO model)
 {
     if (model == null)
     {
         return Forbid();
     }
     var list = SignalRMessageGroups.UserGroups.Where(m => m.GroupName == model.GroupName);
     foreach (var item in list)
     {
         await _hubContext.Clients.Client(item.ConnectionId).SendAsync(model.GroupName, model.MsgJson);
     }
     return Ok();
 }

*********/
