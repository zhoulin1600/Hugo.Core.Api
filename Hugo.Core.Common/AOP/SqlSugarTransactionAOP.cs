using Castle.DynamicProxy;
using Hugo.Core.Common.Filter;
using Hugo.Core.Common.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.AOP
{
    /// <summary>
    /// SqlSugar数据库事务拦截器，继承IInterceptor接口
    /// </summary>
    public class SqlSugarTransactionAOP : IInterceptor
    {
        private readonly ISqlSugarFactory _sqlSugarFactory;
        public SqlSugarTransactionAOP(ISqlSugarFactory sqlSugarFactory)
        {
            _sqlSugarFactory = sqlSugarFactory;
        }

        /// <summary>
        /// 实例化IInterceptor唯一方法 
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;

            //对当前方法的特性验证
            if (method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(SqlSugarTransactionAttribute)) is SqlSugarTransactionAttribute)
            {
                try
                {
                    Console.WriteLine($"启动事务 - Begin Transaction");
                    _sqlSugarFactory.BeginTran();

                    invocation.Proceed();
                    // 异步获取异常，先执行
                    if (IsAsyncMethod(invocation.Method))
                    {
                        var result = invocation.ReturnValue;
                        if (result is Task)
                        {
                            Task.WaitAll(result as Task);
                        }
                    }

                    Console.WriteLine($"提交事务 - Commit Transaction");
                    _sqlSugarFactory.CommitTran();
                }
                catch (Exception)
                {
                    Console.WriteLine($"回滚事务 - Rollback Transaction");
                    _sqlSugarFactory.RollbackTran();
                }
            }
            else
            {
                // 直接执行被拦截方法
                invocation.Proceed();
            }

        }

        /// <summary>
        /// 判断是否异步方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(Task) || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

    }
}
