using Hugo.Core.DataView.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统角色信息-编辑
    /// </summary>
    public class Sys_Role_Edit
    {
        /// <summary>
        /// <para>属性描述：主键ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "未指定角色")]
        public string Id { get; set; }

        /// <summary>
        /// <para>属性描述：角色名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        [MaxLength(20, ErrorMessage = "角色名称超出最大长度")]
        public string RoleName { get; set; }

        /// <summary>
        /// <para>属性描述：角色状态（1：启用，2：禁用）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [EnumDataType(typeof(DefaultStatus), ErrorMessage = "角色状态无效")]
        public int Status { get; set; }

        /// <summary>
        /// <para>属性描述：角色描述</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [MaxLength(255, ErrorMessage = "角色描述超出最大长度")]
        public string Description { get; set; }

        /// <summary>
        /// <para>属性描述：角色序号</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Sort { get; set; } = 0;
    }
}
