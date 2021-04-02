using System;
using System.Threading.Tasks;
using Hugo.Core.Common.Controller;
using Hugo.Core.Common;
using Hugo.Core.Common.Logger;
using Hugo.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Hugo.Core.Common.SignalR;
using IdentityModel;
using NodaTime.Extensions;
using Google.Protobuf.WellKnownTypes;
using Hugo.Core.Common.Auth;
using Hugo.Core.Common.MessageQueue;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// 测试API控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    //[ApiController]
    //[Authorize]
    public class TestController : BaseController
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger loggerHelper;
        private readonly ILog4Logger log4Logger;

        private readonly IHubContext<ChatHub> _hubContext;

        public TestController(IDistributedCache distributedCache,
            ILogger<TestController> loggerHelper, ILog4Logger log4Logger,
            IHubContext<ChatHub> hubContext)
        {
            this.distributedCache = distributedCache;
            this.loggerHelper = loggerHelper;
            this.log4Logger = log4Logger;

            this._hubContext = hubContext;

        }


        /// <summary>
        /// 转为有序的GUID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task TestRedisQueue(string message)
        {
            for (int i = 0; i < 20; i++)
            {
                await RedisHelper.LPushAsync<string>(RedisMQKey.MQTESTKEY, i.ToString());
            }
            
        }


        /// <summary>
        /// Quartz计划任务测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult> TestJobStar()
        {
            Helper_Job.SetCronJob(() => {
                Console.WriteLine($"Quartz计划任务测试 -- ToUnixTimeStamp：{DateTime.Now.ToUnixTimeStamp()},ToJsTimestamp：{DateTime.Now.ToJsTimestamp()}\n");
                //_o_OrderBus.TestJob();
            }, "0/5 * * * * ?");

            return Success();
        }


        /// <summary>
        /// 转为有序的GUID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TestGuid()
        {
            var list = new List<string>();
            list.Add("1"); list.Add("1");
            for (int i = 0; i < 100; i++)
            {
                list.Add(Guid.NewGuid().ToPrimaryKey());
            }

            list.Add($"长度：{list.LastOrDefault().Length}，数量：{list.Count}，去重后数量：{list.Distinct().Count()}");
            //var guid = Guid.NewGuid();
            //var t1 = guid.To16String();
            //var t2 = guid.ToLongString();
            //var t3 = guid.ToPrimaryKey();
            //var result = new
            //{
            //    t1,t2,t3
            //};

            return Ok(list);
        }

        #region JWT

        /// <summary>
        /// JWT登录接口授权
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = GlobalData.AUTH_MANAGER)]
        public async Task<IActionResult> TestJWTManager()
        {
            return Ok(true);
        }

        /// <summary>
        /// JWT登录接口授权
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = GlobalData.AUTH_ADMIN)]
        public async Task<IActionResult> TestJWTAdmin()
        {

            return Ok(true);
        }

        /// <summary>
        /// JWT登录获取Role和Token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult> TestJWTLogin(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool success = false;

            string role = "Admin";

            if (role != null && name != "")
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                JwtTokenData tokenModel = new JwtTokenData { UserId = "123", Role = role };
                jwtStr = AccessToken.IssueJwtToken(tokenModel);//登录，获取到一定规则的 Token 令牌
                success = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }

            //_result.Success = success;
            //_result.Data = jwtStr;
            return Success<string>(jwtStr);
        }

        #endregion

        /// <summary>
        /// 请求头测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestRequestHeader()
        {
            //方式1 - 直接使用
            //string customPara = Request.Headers["CustomPara"].ToString();
            //方式2 - BaseController获取
            string customPara = CustomPara;

            return Ok(customPara);
        }

        /// <summary>
        /// Automapper测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestAutomapper(string account)
        {
            var data = new DataModel.Sys_User { UserName = account, Id = Guid.NewGuid().ToString("N") };
            //var u = data.MapTo<DataView.View_Sys_User>();
            return Ok();
        }

        /// <summary>
        /// 日志测试（ILogger）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestLogging1()
        {
            //过滤级别 FATAL > ERROR > WARN > INFO > DEBUG
            await Task.Run(() =>
            {
                loggerHelper.LogDebug("自定义LogDebug日志");
                loggerHelper.LogInformation("自定义LogInformation日志");
                loggerHelper.LogWarning("自定义LogWarning日志");
                loggerHelper.LogError(new Exception("自定义异常"), "自定义LogError日志");
                loggerHelper.LogTrace(new Exception("自定义跟踪"), "自定义LogTrace日志");
                loggerHelper.LogCritical(new Exception("自定义跟踪"), "自定义LogCritical日志");
            });
            //throw new Exception("自定义全局异常");
            return Ok();
        }

        /// <summary>
        /// 日志测试（log4net）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestLogging2()
        {
            //过滤级别 FATAL > ERROR > WARN > INFO > DEBUG
            await Task.Run(() =>
            {
                log4Logger.Debug("自定义bug日志");
                log4Logger.Info("自定义消息日志");
                log4Logger.Error("自定义错误日志",new Exception("自定义异常"));
            });
            //throw new Exception("自定义全局异常");
            return Ok();
        }

        /// <summary>
        /// 日志测试（全局）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestLogging3(string account)
        {
            //过滤级别 FATAL > ERROR > WARN > INFO > DEBUG
            int i = 0;
            var s = 2 / i;
            return Ok();
        }

        /// <summary>
        /// SignalR测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult> TestSignalR()
        {
            var guid = Guid.NewGuid().ToNString();

            await Task.Run(() =>
            {
                // ReceiveUpdate：客户端定义的方法名
                // guid：消息
                _hubContext.Clients.All.SendAsync("ReceiveUpdate", guid).Wait();
            });

            return Success(guid);
        }

        /// <summary>
        /// Redis缓存测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestRedisCache()
        {
            await distributedCache.SetObjectAsync("Test:distributedCache", "这里使用distributedCache缓存Set");
            await RedisHelper.SetAsync("Test:RedisHelper", "这里使用RedisHelper缓存Set");

            var distributedCacheGet = await distributedCache.GetObjectAsync<string>("Test:distributedCache");
            var redisHelperGet = await RedisHelper.GetAsync<string>("Test:RedisHelper");

            var result = new { distributedCacheGet, redisHelperGet };
            return Ok(result);
        }

        /// <summary>
        /// 空响应测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ContentResult> TestNonResult()
        {
            ApiResult t = new ApiResult();
            t.StatusCode = 100;
            //return t;
            return JsonContent("123");
        }

        /// <summary>
        /// 雪花ID测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestSnowflakeId(long datacenterId, long workerId)
        {
            //List<string> ids = new List<string>();
            //Helper_PrimaryKey primaryKey = new Helper_PrimaryKey(datacenterId, workerId);

            //for (int i = 0; i < 10; i++)
            //{
            //    ids.Add(primaryKey.NextId().ToString());
            //}
            ////string msg = $"总数：{ids.Count},去重总数：{ids.Distinct().Count()}";


            List<string> ids = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                ids.Add(Guid.NewGuid().ToPrimaryKey());
            }
            string msg = $"总数：{ids.Count},去重总数：{ids.Distinct().Count()}";

            return Ok(new { ids
            , msg});

            //ApiResult t = new ApiResult();
            //t.StatusCode = 100;
            ////return t;
            //return JsonContent("123");
        }

        /// <summary>
        /// 时间转换测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestDateTime(long datacenterId, long workerId)
        {
            var time = DateTime.Now;
            string msg = //$"ToBinary：{time.ToBinary()}\r\n" +
                //$"ToBytes：{time.ToBytes()}\r\n" +
                $"ToCstTime：{time.ToCstTime()}\r\n" +
                $"ToFileTime：{time.ToFileTime()}\r\n" +
                $"ToFileTimeUtc：{time.ToFileTimeUtc()}\r\n" +
                $"ToJson：{time.ToJson()}\r\n" +
                $"ToJsTimestamp：{time.ToJsTimestamp()}\r\n" +
                $"ToOADate：{time.ToOADate()}\r\n" +
                //$"ToUniversalTime：{time.ToUniversalTime()}\r\n" +
                $"ToUnixTimeStamp：{time.ToUnixTimeStamp()}\r\n" +
                //$"ToXmlStr：{time.ToXmlStr()}\r\n" +
                $"ToEpochTime：{time.ToEpochTime()}\r\n" +
                //$"EntityToJson：{time.EntityToJson()}\r\n" +
                //$"ToInstant：{time.ToInstant()}\r\n" +
                //$"ToTimestamp：{time.ToTimestamp()}\r\n" +
                $"JsGetTime：{time.JsGetTime()}\r\n";
            
            return Ok(msg);

            //ApiResult t = new ApiResult();
            //t.StatusCode = 100;
            ////return t;
            //return JsonContent("123");
        }

        #region Enum

        /// <summary>
        /// 枚举列表测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> TestEnum()
        {
            return Helper_Enum.ToOptionList(typeof(Member_Identity));
        }
        /// <summary>
        /// 会员身份（1：游客，2：商家，3：主播）
        /// </summary>
        public enum Member_Identity : int
        {
            /// <summary>
            /// 游客
            /// </summary>
            [Description("游客")]
            游客 = 1,

            /// <summary>
            /// 商家
            /// </summary>
            [Description("商家")]
            商家 = 2,

            /// <summary>
            /// 主播
            /// </summary>
            [Description("主播")]
            主播 = 3,
        }

        #endregion

        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Hello World");
        }

    }
}