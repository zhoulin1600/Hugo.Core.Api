using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hugo.Core.DataView.Enum
{
    /// <summary>
    /// 系统应用类型
    /// </summary>
    public enum SysApplicationType : int
    {
        /// <summary>
        /// 默认应用
        /// </summary>
        [Description("默认应用")]
        默认应用 = 1
    }



    /// <summary>
    /// 系统用户类型
    /// </summary>
    public enum SysUserType : int
    {
        /// <summary>
        /// 系统用户
        /// </summary>
        [Description("系统用户")]
        系统用户 = 1
    }

    
}
