using Castle.DynamicProxy;
using Hugo.Core.Common.Filter;
using Hugo.Core.Common.Http;
using Hugo.Core.Common.ORM;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.Common.AOP
{
    /// <summary>
    /// 用户操作日志拦截器，继承IInterceptor接口
    /// </summary>
    public class OperationLogAOP : IInterceptor
    {
        // 依赖注入
        private readonly IHttpContextUser httpContextUser;
        private readonly IServiceProvider serviceProvider;

        public OperationLogAOP(IHttpContextUser httpContextUser, IServiceProvider serviceProvider)
        {
            this.httpContextUser = httpContextUser;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 实例化IInterceptor唯一方法
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //if (method.ReturnType == typeof(void) || method.ReturnType == typeof(Task))
            //{
            //    // 执行被拦截方法（方法无返回数据）
            //    invocation.Proceed();
            //    return;
            //}
            // 对当前方法的特性验证
            var operationLogAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(OperationLogAttribute)) as OperationLogAttribute;
            
            if (operationLogAttribute != null)
            {
                var log = new Sys_Log
                {
                    IP = httpContextUser.ClientIP,
                    //IP = Helper_IP.GetLocalIp(),
                    //OperatorId = httpContextUser.UserId ?? "",
                    //OperatorName = httpContextUser.UserName ?? "",
                    LogType = (int)operationLogAttribute.LogType,
                    LogContent = $"{operationLogAttribute.LogType.GetDescription()}：{operationLogAttribute.LogContent}"
                };

                // 执行被拦截方法
                invocation.Proceed();

                if (operationLogAttribute.LogType == OperationLogType.授权登录)
                {
                    object response;
                    //Type type = invocation.ReturnValue?.GetType();
                    var type = invocation.Method.ReturnType;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if (response == null)
                        return;
                    ApiResult result = response.ChangeType<ApiResult>();
                    if (result.StatusCode != 200)
                        return;
                    log.OperatorId = "";// httpContextUser?.UserId?.ToString();
                    log.OperatorName = invocation.Arguments[0].ToString();
                    log.LogContent = $"{operationLogAttribute.LogType.GetDescription()}：用户 {invocation.Arguments[0].ToString()} {operationLogAttribute.LogContent}";
                }
                else
                {
                    log.OperatorId = httpContextUser?.UserId?.ToString();
                    log.OperatorName = httpContextUser?.UserName?.ToString();
                }


                Task.Factory.StartNew(async () =>
                {
                    using (var scop = serviceProvider.CreateScope())
                    {
                        var db = scop.ServiceProvider.GetService<ISqlSugarClient>();
                        await db.Insertable(log).ExecuteCommandAsync();
                    }
                }, TaskCreationOptions.LongRunning);
            }
            else
            {
                // 执行被拦截方法（方法无OperationLog特性加持）
                invocation.Proceed();
            }
        }

        /// <summary>
        /// 系统日志信息
        /// </summary>
        [SugarTable("Sys_Log")]
        public class Sys_Log
        {
            /// <summary>
            /// <para>属性描述：IP地址</para>
            /// <para>默认数据：</para>
            /// <para>是否可空：True</para>
            /// </summary>
            public string IP { get; set; }

            /// <summary>
            /// <para>属性描述：操作人ID</para>
            /// <para>默认数据：</para>
            /// <para>是否可空：True</para>
            /// </summary>
            public string OperatorId { get; set; }

            /// <summary>
            /// <para>属性描述：操作人名称</para>
            /// <para>默认数据：</para>
            /// <para>是否可空：True</para>
            /// </summary>
            public string OperatorName { get; set; }

            /// <summary>
            /// <para>属性描述：日志类型（1：新增，2：编辑，3：删除，4：查询）</para>
            /// <para>默认数据：</para>
            /// <para>是否可空：True</para>
            /// </summary>
            public int? LogType { get; set; }

            /// <summary>
            /// <para>属性描述：日志内容</para>
            /// <para>默认数据：</para>
            /// <para>是否可空：True</para>
            /// </summary>
            public string LogContent { get; set; }
        }
    }
}
