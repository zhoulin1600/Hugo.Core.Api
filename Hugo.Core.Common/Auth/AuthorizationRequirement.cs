using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Hugo.Core.Common.Auth
{
    /// <summary>
    /// 授权条件配置信息
    /// </summary>
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// 颁发者签名验证凭据
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }

        /// <summary>
        /// 权限详情集合(Role和Url的关系)
        /// </summary>
        public List<AuthorityItem> Authorities { get; set; }

        /// <summary>
        /// 请求Token地址-登录Action
        /// </summary>
        public string LoginAction { get; set; } = "/Api/Account/Login";

        /// <summary>
        /// 无权限跳转地址-404Action
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 授权条件自定义模型
        /// </summary>
        /// <param name="issuer">颁发者</param>
        /// <param name="audience">订阅者</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="signingCredentials">颁发者签名验证凭据</param>
        /// <param name="claimType">认证授权类型</param>
        /// <param name="authorities">权限详情集合</param>
        public AuthorizationRequirement(string issuer, string audience, TimeSpan expiration, SigningCredentials signingCredentials, string claimType, List<AuthorityItem> authorities)
        {
            this.Issuer = issuer;
            this.Audience = audience;
            this.Expiration = expiration;
            this.SigningCredentials = signingCredentials;
            this.ClaimType = claimType;
            this.Authorities = authorities;
        }

    }
}
