using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Hugo.Core.Common.AutoMapperTool
{
    /// <summary>
    /// 自定义通用对象映射 - 静态泛型缓存
    /// <para>轻量级，高性能</para>
    /// </summary>
    /// <typeparam name="TSource">来源类型</typeparam>
    /// <typeparam name="TResult">映射类型</typeparam>
    public class CustomMapper<TSource, TResult>
    {
        private static Func<TSource, TResult> func = null;

        static CustomMapper()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "parameter");
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            // 遍历属性信息
            foreach (var propertyInfo in typeof(TResult).GetProperties())
            {
                MemberExpression memberExpression = Expression.Property(parameterExpression, typeof(TSource).GetProperty(propertyInfo.Name));
                MemberBinding memberBinding = Expression.Bind(propertyInfo, memberExpression);
                memberBindings.Add(memberBinding);
            }
            // 遍历字段信息
            foreach (var fieldInfo in typeof(TResult).GetFields())
            {
                MemberExpression memberExpression = Expression.Field(parameterExpression, typeof(TSource).GetField(fieldInfo.Name));
                MemberBinding memberBinding = Expression.Bind(fieldInfo, memberExpression);
                memberBindings.Add(memberBinding);
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TResult)), memberBindings);
            Expression<Func<TSource, TResult>> lambda = Expression.Lambda<Func<TSource, TResult>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
            func = lambda.Compile();
        }

        /// <summary>
        /// 获取映射
        /// </summary>
        /// <param name="source">来源对象</param>
        /// <returns>映射对象</returns>
        public static TResult Map(TSource source)
        {
            return func.Invoke(source);
        }

    }
}
