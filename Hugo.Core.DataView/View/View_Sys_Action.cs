using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统功能信息
    /// </summary>
    public partial class View_Sys_Action : Base.BaseDataView
    {
        public View_Sys_Action()
        {
            this.CreateTime = DateTime.Now;
            this.Sort = Convert.ToInt32("0");
            this.IsDeleted = false;
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
        /// <para>属性描述：功能名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// <para>属性描述：菜单ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// <para>属性描述：Api地址</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// <para>属性描述：功能图标</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// <para>属性描述：功能描述</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：功能序号</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// <para>属性描述：是否已删除</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}