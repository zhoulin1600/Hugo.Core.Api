using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统菜单信息
    /// </summary>
    public partial class View_Sys_Menu : Base.BaseDataView
    {
        public View_Sys_Menu()
        {
            this.CreateTime = DateTime.Now;
            this.IsHidden = false;
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
        /// <para>属性描述：菜单名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// <para>属性描述：上级ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// <para>属性描述：组件名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// <para>属性描述：链接地址</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// <para>属性描述：菜单图标</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// <para>属性描述：是否隐藏</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public bool? IsHidden { get; set; }

        /// <summary>
        /// <para>属性描述：菜单描述</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：菜单序号</para>
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