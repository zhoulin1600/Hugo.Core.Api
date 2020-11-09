using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hugo.Core.DataView.DTO
{
    /// <summary>
    /// 系统用户信息-添加
    /// </summary>
    public class Sys_User_Add
    {
        /// <summary>
        /// <para>属性描述：用户名（唯一）</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(20, ErrorMessage = "用户名超出最大长度")]
        public string UserName { get; set; }

        /// <summary>
        /// <para>属性描述：登录密码</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        [Required(ErrorMessage = "登录密码不能为空")]
        [DataType(DataType.Password, ErrorMessage = "登录密码无效")]
        [StringLength(20, ErrorMessage = "登录密码长度介于6-20位", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
