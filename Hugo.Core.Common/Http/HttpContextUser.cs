using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Hugo.Core.Common.Http
{
    /// <summary>
    /// HttpContext用户信息服务实现
    /// </summary>
    public class HttpContextUser : IHttpContextUser
    {
        private readonly IHttpContextAccessor accessor;

        public HttpContextUser(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIP => accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName => GetUserName();
        private string GetUserName()
        {
            if (IsAuthenticated())
                return accessor.HttpContext.User.Identity.Name;
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                    return GetUserInfoFromToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault().ToString();
            }
            return "";
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId => GetClaimValueByType("jti").FirstOrDefault().ToString();

        /// <summary>
        /// 获取Tonken字符串
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return accessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        /// <summary>
        /// 是否已通过身份验证
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public List<string> GetUserInfoFromToken(string claimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(GetToken()))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(GetToken());

                return (from item in jwtToken.Claims
                        where item.Type == claimType
                        select item.Value).ToList();
            }
            else
            {
                return new List<string>() { };
            }
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string claimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == claimType
                    select item.Value).ToList();
        }


    }
}
