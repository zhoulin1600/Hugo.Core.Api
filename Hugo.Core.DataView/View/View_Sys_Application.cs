using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统应用信息
    /// </summary>
    public partial class View_Sys_Application : Base.BaseDataView
    {
        public View_Sys_Application()
        {
            this.CreateTime = DateTime.Now;
            this.AppType = Convert.ToInt32("1");
        }
        
        /// <summary>
        /// <para>属性描述：主键ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
           [SugarColumn(IsPrimaryKey=true)]
        public string Id { get; set; }

        /// <summary>
        /// <para>属性描述：创建时间</para>
        /// <para>默认数据：DateTime.Now</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// <para>属性描述：应用名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// <para>属性描述：应用ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// <para>属性描述：应用密钥</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// <para>属性描述：应用ID（1：默认）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public int? AppType { get; set; }
    }
}