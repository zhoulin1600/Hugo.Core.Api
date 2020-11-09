using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SqlSugar;
using StackExchange.Profiling;

namespace Hugo.Core.Common.ORM
{
    /// <summary>
    /// SqlSugar上下文工厂（生产 SqlSugarClient 实例）
    /// </summary>
    public class SqlSugarFactory : ISqlSugarFactory
    {
        private readonly ConnectionConfig _connectionConfig;
        private readonly ISqlSugarClient _sqlSugarClient;
        private readonly ILogger _logger;

        public SqlSugarFactory(ConnectionConfig connectionConfig, ISqlSugarClient sqlSugarClient, ILogger<SqlSugarFactory> logger)
        {
            this._connectionConfig = connectionConfig;
            this._sqlSugarClient = sqlSugarClient;
            this._logger = logger;
        }

        public SqlSugarClient GetDbContext() => GetDbContext(null, null, null);

        public SqlSugarClient GetDbContext(Action<Exception> onErrorEvent) => GetDbContext(null, null, onErrorEvent);

        public SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent) => GetDbContext(onExecutedEvent, null, null);

        public SqlSugarClient GetDbContext(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent) => GetDbContext(null, onExecutingChangeSqlEvent, null);

        public SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent = null, Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent = null, Action<Exception> onErrorEvent = null)
        {
            //SqlSugarClient db = new SqlSugarClient(_config)
            //{
            //    // SQL打印
            //    Aop =
            //    {
            //        OnExecutingChangeSql = onExecutingChangeSqlEvent,
            //        OnError = onErrorEvent ?? ((Exception ex) => { this._logger.LogError(ex, "ExecuteSql Error"); }),
            //        OnLogExecuted =onExecutedEvent ?? ((string sql, SugarParameter[] pars) =>
            //        {
            //            var keyDic = new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //            this._logger.LogInformation($"ExecuteSql：{keyDic.ToJsonString()}");
            //            //Log4NetHelper.LogInfo($"ExecuteSql：{keyDic.ToJsonString()}");
            //        })
            //    }
            //};
            //return db;

            // SqlSugar AOP拦截处理配置
            _sqlSugarClient.Aop.OnExecutingChangeSql = onExecutingChangeSqlEvent;
            _sqlSugarClient.Aop.OnError = onErrorEvent ?? ((Exception ex) => { this._logger.LogError(ex, ex.Message); });
            _sqlSugarClient.Aop.OnLogExecuted = onExecutedEvent ?? ((string sql, SugarParameter[] parameters) =>
            {
                var information = $"\r\n";
                information += $"执行SQL：{sql}\r\n";
                var paramString = $"{string.Join(", ", parameters.Select(para => para.ParameterName + ": " + para.Value).ToArray())}";
                information += $"执行参数：{paramString}";
                // Sql执行AOP拦截
                if (AppSettings.GetSetting(new string[] { "Interceptor", "SqlSugarSqlAOP", "Enabled" }).ToBool())
                    this._logger.LogInformation(information);
                // MiniProfiler性能分析器
                StackExchange.Profiling.MiniProfiler.Current.CustomTiming("SQL语句", $"{sql}\r\n{paramString}");
            });

            // 必须要as，后边会用到切换数据库操作
            return _sqlSugarClient as SqlSugarClient;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            GetDbContext().BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                GetDbContext().CommitTran();
            }
            catch (Exception ex)
            {
                GetDbContext().RollbackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            GetDbContext().RollbackTran();
        }

    }
}
