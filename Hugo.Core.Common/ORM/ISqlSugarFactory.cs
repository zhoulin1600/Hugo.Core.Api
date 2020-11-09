using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.ORM
{
    /// <summary>
    /// SqlSugar上下文工厂（生产 SqlSugarClient 实例）
    /// </summary>
    public interface ISqlSugarFactory
    {
        SqlSugarClient GetDbContext();

        SqlSugarClient GetDbContext(Action<Exception> onErrorEvent);

        SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent);

        SqlSugarClient GetDbContext(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent);

        SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent = null, Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent = null, Action<Exception> onErrorEvent = null);

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
