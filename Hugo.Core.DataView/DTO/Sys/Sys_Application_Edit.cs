using Hugo.Core.DataView.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统应用信息-编辑
    /// </summary>
    public class Sys_Application_Edit
    {
        /// <summary>
        /// <para>属性描述：主键ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "未指定应用")]
        public string Id { get; set; }

        /// <summary>
        /// <para>属性描述：应用名称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "应用名称不能为空")]
        [MaxLength(20, ErrorMessage = "应用名称超出最大长度")]
        public string AppName { get; set; }

        /// <summary>
        /// <para>属性描述：应用ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "应用ID不能为空")]
        [MaxLength(20, ErrorMessage = "应用ID超出最大长度")]
        public string AppId { get; set; }

        /// <summary>
        /// <para>属性描述：应用密钥</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [MaxLength(50, ErrorMessage = "应用密钥超出最大长度")]
        public string AppSecret { get; set; }

        /// <summary>
        /// <para>属性描述：应用ID（1：默认）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [EnumDataType(typeof(SysApplicationType), ErrorMessage = "应用类型无效")]
        public int AppType { get; set; }
    }
}
