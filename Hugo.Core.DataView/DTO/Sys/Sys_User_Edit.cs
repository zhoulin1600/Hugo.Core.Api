using Hugo.Core.DataView.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统用户信息-编辑
    /// </summary>
    public class Sys_User_Edit
    {
        /// <summary>
        /// <para>属性描述：主键ID</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "未指定用户")]
        public string Id { get; set; }

        /// <summary>
        /// <para>属性描述：用户状态（1：启用，2：禁用）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [EnumDataType(typeof(DefaultStatus), ErrorMessage = "用户状态无效")]
        public int Status { get; set; }

        /// <summary>
        /// <para>属性描述：用户类型（1：系统用户）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [EnumDataType(typeof(SysUserType), ErrorMessage = "用户类型无效")]
        public int Type { get; set; }

        /// <summary>
        /// <para>属性描述：手机号码</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [RegularExpression(@"^[1]{1}[3,4,5,6,7,8,9]{1}\d{9}$", ErrorMessage = "手机号码格式错误")]
        public string Phone { get; set; }

        /// <summary>
        /// <para>属性描述：真实性名</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [MaxLength(20, ErrorMessage = "真实性名超出最大长度")]
        public string RealName { get; set; }

        /// <summary>
        /// <para>属性描述：用户昵称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [MaxLength(20, ErrorMessage = "用户昵称超出最大长度")]
        public string NickName { get; set; }

        /// <summary>
        /// <para>属性描述：用户头像</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [DataType(DataType.ImageUrl, ErrorMessage = "用户头像无效")]
        public string HeadImage { get; set; }

        /// <summary>
        /// <para>属性描述：用户备注</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        [MaxLength(255, ErrorMessage = "用户备注超出最大长度")]
        public string Remark { get; set; }

    }
}
