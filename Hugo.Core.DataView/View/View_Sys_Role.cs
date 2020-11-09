using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统角色信息
    /// </summary>
    public partial class View_Sys_Role : Base.BaseDataView
    {
        public View_Sys_Role()
        {
            this.CreateTime = DateTime.Now;
            this.Status = Convert.ToInt32("1");
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
        /// <para>属性描述：角色名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// <para>属性描述：角色状态（1：启用，2：禁用）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// <para>属性描述：角色描述</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：角色序号</para>
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