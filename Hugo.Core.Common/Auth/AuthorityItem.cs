namespace Hugo.Core.Common.Auth
{
    /// <summary>
    /// 权限详情自定义项
    /// </summary>
    public class AuthorityItem
    {
        /// <summary>
        /// 用户或角色或其他凭据名称
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
