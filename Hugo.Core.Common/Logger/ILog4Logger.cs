using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Logger
{
    /// <summary>
    /// 日志帮助
    /// <para>NuGet：Install-Package Microsoft.Extensions.Logging.Log4Net.AspNetCore</para>
    /// </summary>
    public interface ILog4Logger
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        void Debug(object message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        void Info(object message);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        void Warn(object message);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        void Error(object message);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">exception</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        void Fatal(object message);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        void Fatal(object message, Exception exception);

    }
}
