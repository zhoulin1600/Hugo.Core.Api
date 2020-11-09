using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统功能信息-添加
    /// </summary>
    public class Sys_Action_Add
    {
        /// <summary>
        /// <para>属性描述：功能名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "功能名称不能为空")]
        [MaxLength(20, ErrorMessage = "功能名称超出最大长度")]
        public string ActionName { get; set; }

        /// <summary>
        /// <para>属性描述：菜单ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "未指定菜单")]
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
        [MaxLength(255, ErrorMessage = "功能描述超出最大长度")]
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：功能序号</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Sort { get; set; }
    }
}
