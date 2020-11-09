using Castle.DynamicProxy;
using CSRedis;
using Hugo.Core.Common.Filter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.Common.AOP
{
    /// <summary>
    /// Redis缓存数据拦截器
    /// </summary>
    public class CacheRedisAOP : CacheBaseAOP
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly CSRedisClient _redisCache;

        public CacheRedisAOP()
        {
            _redisCache = RedisHelper.Instance;
        }

        /// <summary>
        /// 实例化IInterceptor唯一方法
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if (method.ReturnType == typeof(void) || method.ReturnType == typeof(Task))
            {
                // 执行被拦截方法（方法无返回数据）
                invocation.Proceed();
                return;
            }
            // 对当前方法的特性验证
            var cachingAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) as CachingAttribute;

            if (cachingAttribute != null)
            {
                // 获取自定义缓存键
                var cacheKey = cachingAttribute.CustomKey ?? CustomCacheKey(invocation);
                // 根据key获取redis缓存数据
                var cacheValue = _redisCache.Get(cacheKey);
                if (cacheValue != null)
                {
                    // 将当前获取到的缓存数据赋值给当前执行方法
                    Type returnType;
                    if (typeof(Task).IsAssignableFrom(method.ReturnType))
                    {
                        returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
                    }
                    else
                    {
                        returnType = method.ReturnType;
                    }

                    dynamic _result = Newtonsoft.Json.JsonConvert.DeserializeObject(cacheValue, returnType);
                    invocation.ReturnValue = typeof(Task).IsAssignableFrom(method.ReturnType) ? Task.FromResult(_result) : _result;
                    return;
                }
                // 执行被拦截方法
                invocation.Proceed();

                // 缓存返回数据
                if (!string.IsNullOrWhiteSpace(cacheKey))
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
                    if (response == null) response = string.Empty;

                    _redisCache.Set(cacheKey, response, TimeSpan.FromMinutes(cachingAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                // 执行被拦截方法（方法无Caching特性加持）
                invocation.Proceed();
            }
        }
    }
}
