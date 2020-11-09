using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataModel
{
    /// <summary>
    /// 系统日志信息
    /// </summary>
    [SugarTable("Sys_Log")]
    public class Sys_Log : Base.BaseDataModel
    {
        public Sys_Log()
        {
            this.CreateTime = DateTime.Now;
        }
        
        /// <summary>
        /// <para>属性描述：主键ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public long Id { get; set; }

        /// <summary>
        /// <para>属性描述：创建时间</para>
        /// <para>默认数据：DateTime.Now</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// <para>属性描述：IP地址</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// <para>属性描述：操作人ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// <para>属性描述：操作人名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// <para>属性描述：日志类型（1：新增，2：编辑，3：删除，4：查询，5：登录）</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// <para>属性描述：日志内容</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string LogContent { get; set; }
    }
}