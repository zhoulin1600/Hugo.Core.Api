using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统日志信息扩展
    /// </summary>
    public partial class View_Sys_Log
    {
        /// <summary>
        /// <para>属性描述：日志类型名称（1：新增，2：编辑，3：删除，4：查询）</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string LogTypeName { get; set; }
    }
}
