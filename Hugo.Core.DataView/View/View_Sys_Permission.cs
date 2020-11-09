using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统权限信息
    /// </summary>
    public partial class View_Sys_Permission : Base.BaseDataView
    {
        public View_Sys_Permission()
        {
            this.CreateTime = DateTime.Now;
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
        /// <para>属性描述：角色ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// <para>属性描述：菜单ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// <para>属性描述：功能集合</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string ActionIds { get; set; }

        /// <summary>
        /// <para>属性描述：是否已删除</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}