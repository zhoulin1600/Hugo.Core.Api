using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hugo.Core.Common.Http
{
    /// <summary>
    /// HttpContext用户信息服务接口
    /// </summary>
    public interface IHttpContextUser
    {
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        string ClientIP { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 获取Tonken字符串
        /// </summary>
        /// <returns></returns>
        string GetToken();

        /// <summary>
        /// 是否已通过身份验证
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        List<string> GetUserInfoFromToken(string claimType);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        List<string> GetClaimValueByType(string claimType);

    }
}
