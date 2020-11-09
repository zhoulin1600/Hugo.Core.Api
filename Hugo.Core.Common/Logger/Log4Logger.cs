using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace Hugo.Core.Common.Logger
{
    /// <summary>
    /// 日志帮助类
    /// <para>NuGet：Install-Package Microsoft.Extensions.Logging.Log4Net.AspNetCore</para>
    /// </summary>
    public class Log4Logger : ILog4Logger
    {
        private readonly ConcurrentDictionary<LogLevel, ILog> _loggers = new ConcurrentDictionary<LogLevel, ILog>();

        /// <summary>
        /// 获取日志记录器
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        private ILog GetLogger(LogLevel logLevel)
        {
            if (_loggers.ContainsKey(logLevel))
            {
                return _loggers[logLevel];
            }
            else
            {
                var log4netRepositoryName = $"Log4net{logLevel}Repository";//日志的仓储名，也就是当前项目名
                var log4netConfigFilePath = AppContext.BaseDirectory + "Logger\\log4net.config";//log4net的配置文件
                if (!File.Exists(log4netConfigFilePath))
                    log4netConfigFilePath = "log4net.config";//指定配置文件不存在则更改为默认启动位置
                var log4netConfigFile = new FileInfo(log4netConfigFilePath);
                if(log4netConfigFile == null)
                    throw new Exception("未找到 log4net.config 配置文件");
                var log4netRepository = LogManager.CreateRepository(log4netRepositoryName);
                log4net.Config.XmlConfigurator.Configure(log4netRepository, log4netConfigFile);

                ILog logger = LogManager.GetLogger(log4netRepositoryName, logLevel.ToString());
                _loggers.TryAdd(logLevel, logger);
                return logger;
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        public void Debug(object message)
        {
            ILog logger = GetLogger(LogLevel.Debug);
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        public void Debug(object message, Exception exception)
        {
            ILog logger = GetLogger(LogLevel.Debug);
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message, exception);
            }
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        public void Info(object message)
        {
            ILog logger = GetLogger(LogLevel.Information);
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        public void Info(object message, Exception exception)
        {
            ILog logger = GetLogger(LogLevel.Information);
            if (logger.IsInfoEnabled)
            {
                logger.Info(message, exception);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        public void Warn(object message)
        {
            ILog logger = GetLogger(LogLevel.Warning);
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        public void Warn(object message, Exception exception)
        {
            ILog logger = GetLogger(LogLevel.Warning);
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        public void Error(object message)
        {
            ILog logger = GetLogger(LogLevel.Error);
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        public void Error(object message, Exception exception)
        {
            ILog logger = GetLogger(LogLevel.Error);
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, exception);
            }
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        public void Fatal(object message)
        {
            ILog logger = GetLogger(LogLevel.Error);
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Fatal(object message, Exception exception)
        {
            ILog logger = GetLogger(LogLevel.Error);
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, exception);
            }
        }

    }
}
