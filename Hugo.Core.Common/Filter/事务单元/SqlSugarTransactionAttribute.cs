using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// SqlSugar数据库事务操作特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class SqlSugarTransactionAttribute : BaseActionFilterAsync
    {
    }
}