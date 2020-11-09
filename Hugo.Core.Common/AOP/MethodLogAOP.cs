using Castle.DynamicProxy;
using Hugo.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hugo.Core.Common.AOP
{
    /// <summary>
    /// Method方法日志拦截器，继承IInterceptor接口
    /// </summary>
    public class MethodLogAOP : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MethodLogAOP(ILogger<MethodLogAOP> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 实例化IInterceptor接口唯一方法
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            // 当前操作用户
            string userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            // 记录被拦截方法的相关信息
            var dataIntercept = $"\r\n" +
                $"操作用户：{userName}\r\n" +
                $"执行方法：{invocation.TargetType.FullName} {invocation.Method.Name}\r\n" +
                $"执行参数：{string.Join(", ", invocation.Arguments.Select(para => (para ?? "").ToJson()).ToArray())}\r\n";
            try
            {
                // 在被拦截的方法执行完毕后 继续执行当前方法
                invocation.Proceed();

                // 判断方法是否异步（异步则需要使用异步的方式处理方法）
                if (IsAsyncMethod(invocation.Method))
                {
                    #region 方案一
                    if (invocation.Method.ReturnType == typeof(Task))
                    {
                        // 无返回的异步方法 - Task
                        invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                            (Task)invocation.ReturnValue,
                            async () => await SuccessActionAsync(invocation, dataIntercept),/*成功时执行*/
                            ex =>
                            {
                                // 异步方法的异常日志
                                _logger.LogError(ex, dataIntercept);
                            });
                    }
                    else
                    {
                        // 有返回的异步方法 - Task<TResult>
                        invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                        invocation.Method.ReturnType.GenericTypeArguments[0],
                        invocation.ReturnValue,
                            async (result) => await SuccessActionAsync(invocation, dataIntercept, result),/*成功时执行*/
                            ex =>
                            {
                                // 异步方法的异常日志
                                _logger.LogError(ex, dataIntercept);
                            });
                    }
                    #endregion

                    #region 方案二
                    //var type = invocation.Method.ReturnType;
                    //var resultProperty = type.GetProperty("Result");
                    //dataIntercept += $"返回结果：{JsonConvert.SerializeObject(resultProperty.GetValue(invocation.ReturnValue))}";

                    //_logger.LogInformation(dataIntercept);
                    #endregion
                }
                else
                {
                    dataIntercept += $"返回结果：{invocation.ReturnValue}";
                    _logger.LogInformation(dataIntercept);
                }

            }
            catch (Exception ex)
            {
                // 同步方法的异常日志
                _logger.LogError(ex, dataIntercept);
            }
        }

        /// <summary>
        /// 当前方法是否异步方法
        /// </summary>
        /// <param name="method">执行的方法</param>
        /// <returns></returns>
        private static bool IsAsyncMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(Task) || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        /// <summary>
        /// 异步方法成功执行处理
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        /// <param name="dataIntercept">记录被拦截方法的相关信息</param>
        /// <param name="result">返回结果</param>
        /// <returns></returns>
        private async Task SuccessActionAsync(IInvocation invocation, string dataIntercept, object result = null)
        {
            invocation.ReturnValue = result;
            var type = invocation.Method.ReturnType;
            if (typeof(Task).IsAssignableFrom(type))
            {
                var resultProperty = type.GetProperty("Result");
                //类型错误 都可以不要invocation参数，直接将result序列化保存到日记中
                dataIntercept += $"返回结果：{JsonConvert.SerializeObject(invocation.ReturnValue)}";
            }
            else
            {
                dataIntercept += $"返回结果：{invocation.ReturnValue}";
            }

            await Task.Run(() =>
            {
                _logger.LogInformation(dataIntercept);
                //Parallel.For(0, 1, e =>
                //{
                //    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                //});
            });
        }

    }

    /// <summary>
    /// 内部异步处理帮助
    /// </summary>
    internal static class InternalAsyncHelper
    {
        /// <summary>
        /// 无返回的异步方法
        /// </summary>
        /// <param name="actualReturnValue"></param>
        /// <param name="postAction"></param>
        /// <param name="finalAction"></param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (exception != null)
                    finalAction(exception);
            }
        }

        public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<object, Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                var result = await actualReturnValue;
                await postAction(result);
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (exception != null)
                    finalAction(exception);
            }
        }

        public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<object, Task> action, Action<Exception> finalAction)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, action, finalAction });
        }
    }


}
