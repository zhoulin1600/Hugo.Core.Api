using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.ORM
{
    /// <summary>
    /// 上下文工厂（生产SqlSugarClient实例）
    /// </summary>
    public class DbFactory : IDbFactory
    {
        private readonly ILogger _logger;
        private readonly ConnectionConfig _config;
        private readonly ISqlSugarClient _sqlSugarClient;

        public DbFactory(ConnectionConfig config, ILogger<DbFactory> logger, ISqlSugarClient sqlSugarClient)
        {
            this._logger = logger;
            this._config = config;
            this._sqlSugarClient = sqlSugarClient;
            //this._sqlSugarClient.CurrentConnectionConfig = config;
        }

        public SqlSugarClient GetDbContext(Action<Exception> onErrorEvent) => GetDbContext(null, null, onErrorEvent);

        public SqlSugarClient GetDbContext(Action<string, SugarParameter[]> onExecutedEvent) => GetDbContext(onExecutedEvent, null);

        public SqlSugarClient GetDbContext(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> onExecutingChangeSqlEvent) => GetDbContext(null, onExecutingChangeSqlEvent);
        
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
            SqlSugarClient db = GetDbClient();
            db.Aop.OnExecutingChangeSql = onExecutingChangeSqlEvent;
            db.Aop.OnError = onErrorEvent ?? ((Exception ex) => { this._logger.LogError(ex, "ExecuteSql Error"); });
            db.Aop.OnLogExecuted = onExecutedEvent ?? ((string sql, SugarParameter[] pars) =>
              {
                  //MiniProfiler.Current.CustomTiming("SQL：", GetParas(p) + "【SQL语句】：" + sql);
                  //LogLock.OutSql2Log("SqlLog", new string[] { GetParas(p), "【SQL语句】：" + sql });
                  this._logger.LogInformation($"【SQL语句】：{sql}\r\n{GetParas(pars)}");
                  //var keyDic = new KeyValuePair<string, SugarParameter[]>(sql, pars);
                  //this._logger.LogInformation($"ExecuteSql：{keyDic.ToJsonString()}");
                  //Log4NetHelper.LogInfo($"ExecuteSql：{keyDic.ToJsonString()}");
              });

            return db;
        }
        private static string GetParas(SugarParameter[] pars)
        {
            string key = "【SQL参数】：";
            foreach (var param in pars)
            {
                key += $"{param.ParameterName}:{param.Value}\r";
            }

            return key;
        }

        /// <summary>
        /// 获取 SqlSugarClient 实例，保证实例的唯一性
        /// </summary>
        /// <returns>SqlSugarClient实例</returns>
        public SqlSugarClient GetDbClient()
        {
            // 必须要as，后边会用到切换数据库操作
            return _sqlSugarClient as SqlSugarClient;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            GetDbClient().BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                GetDbClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().RollbackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            GetDbClient().RollbackTran();
        }
    }
}