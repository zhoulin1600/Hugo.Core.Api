using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Hugo.Core.DataView
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public partial class View_Sys_User : Base.BaseDataView
    {
        public View_Sys_User()
        {
            this.CreateTime = DateTime.Now;
            this.Status = Convert.ToInt32("1");
            this.Type = Convert.ToInt32("1");
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
        /// <para>属性描述：用户名（唯一）</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// <para>属性描述：登录密码</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// <para>属性描述：用户状态（1：启用，2：禁用）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// <para>属性描述：用户类型（1：系统用户）</para>
        /// <para>默认数据：1</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// <para>属性描述：手机号码</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// <para>属性描述：真实性名</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// <para>属性描述：用户昵称</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// <para>属性描述：用户头像</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// <para>属性描述：用户备注</para>
        /// <para>默认数据：</para>
        /// <para>是否可空：True</para>
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// <para>属性描述：是否已删除</para>
        /// <para>默认数据：0</para>
        /// <para>是否可空：False</para>
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}