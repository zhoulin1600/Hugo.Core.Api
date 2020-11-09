using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统菜单信息-添加
    /// </summary>
    public class Sys_Menu_Add
    {
        /// <summary>
        /// <para>属性描述：菜单名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        [MaxLength(20, ErrorMessage = "菜单名称超出最大长度")]
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
        [MaxLength(20, ErrorMessage = "组件名称超出最大长度")]
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
        [MaxLength(255, ErrorMessage = "菜单描述超出最大长度")]
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：菜单序号</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Sort { get; set; } = 0;
    }
}
