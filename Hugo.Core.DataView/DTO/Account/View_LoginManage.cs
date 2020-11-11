using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 管理后台用户名密码登录对象
    /// </summary>
    public class View_LoginManage
    {
        /// <summary>
        /// <para>属性描述：用户名</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string Username { get; set; }

        /// <summary>
        /// <para>属性描述：密码</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
