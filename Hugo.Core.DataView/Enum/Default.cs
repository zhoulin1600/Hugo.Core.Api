using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hugo.Core.DataView.Enum
{
    /// <summary>
    /// 默认状态
    /// </summary>
    public enum DefaultStatus : int
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        启用 = 1,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        禁用 = 2
    }

}
