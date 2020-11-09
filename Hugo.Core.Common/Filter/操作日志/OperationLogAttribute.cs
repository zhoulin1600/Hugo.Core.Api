using System;
using System.ComponentModel;

namespace Hugo.Core.Common.Filter
{
    /// <summary>
    /// 用户操作日志特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class OperationLogAttribute : BaseActionFilterAsync
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public OperationLogType LogType { get; set; } = OperationLogType.查询;

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
    }

    /// <summary>
    /// 系统日志类型
    /// </summary>
    public enum OperationLogType : int
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        新增 = 1,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        编辑 = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        删除 = 3,

        /// <summary>
        /// 查询
        /// </summary>
        [Description("查询")]
        查询 = 4,

        /// <summary>
        /// 授权登录
        /// </summary>
        [Description("授权登录")]
        授权登录 = 5
    }

}
