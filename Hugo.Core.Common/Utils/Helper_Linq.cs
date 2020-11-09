using System;
using System.Linq.Expressions;

namespace Hugo.Core.Common
{
    /// <summary>
    /// Linq操作帮助类
    /// </summary>
    public static class Helper_Linq
    {
        /// <summary>
        /// 创建初始条件为True的表达式（And）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return t => true;
        }

        /// <summary>
        /// 创建初始条件为False的表达式（Or）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return t => false;
        }
    }
}