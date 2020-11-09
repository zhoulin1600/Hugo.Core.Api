using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Hugo.Core.Common.Auth
{
    /// <summary>
    /// 访问令牌类
    /// <para>NuGet：Install-Package IdentityModel</para>
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.Authentication.JwtBearer</para>
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.Authorization</para>
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 颁发JwtToken字符串
        /// </summary>
        /// <param name="tokenModel">令牌模型</param>
        /// <returns>JwtToken字符串</returns>
        public static string IssueJwtToken(JwtTokenData tokenModel)
        {
            // 获取Jwt的配置信息
            string issuer = AppSettings.GetSetting(new string[] { "TokenConfig", "Issuer" });
            string audience = AppSettings.GetSetting(new string[] { "TokenConfig", "Audience" });
            string secretKey = AppSettings.GetSetting(new string[] { "TokenConfig", "SecretKey" });
            int expiration = AppSettings.GetSetting(new string[] { "TokenConfig", "Expiration" }).ToInt();

            var claims = new List<Claim>
            {
                /*
                特别重要：
                1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                */                
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.UserId),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(expiration)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                
                //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            };
            // 可以将一个用户的多个角色全部赋予
            claims.AddRange(tokenModel.Role.Split(',').Select(role => new Claim(ClaimTypes.Role, role)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                signingCredentials: signingCredentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwtToken;
        }

        /// <summary>
        /// 颁发JwtToken字符串
        /// </summary>
        /// <param name="jwtTokenData">JWT令牌数据模型</param>
        /// <param name="authorizationRequirement">授权条件配置信息</param>
        /// <returns>JwtToken字符串</returns>
        public static JwtTokenInfo IssueJwtToken(JwtTokenData jwtTokenData, AuthorizationRequirement authorizationRequirement)
        {
            var dateTime = DateTime.Now;
            var claims = new List<Claim>
            {
                /*
                特别重要：
                1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                */                
                new Claim(JwtRegisteredClaimNames.Jti, jwtTokenData.UserId),
                new Claim(ClaimTypes.Name, jwtTokenData.UserName),
                new Claim(ClaimTypes.UserData, jwtTokenData.UserData),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(authorizationRequirement.Expiration.TotalSeconds).ToString())
                //new Claim(ClaimTypes.Role,jwtTokenData.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            };
            // 可以将一个用户的多个角色全部赋予
            claims.AddRange(jwtTokenData.Role.Split(',').Select(role => new Claim(ClaimTypes.Role, role)));

            // 实例化JwtSecurityToken
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: authorizationRequirement.Issuer,
                audience: authorizationRequirement.Audience,
                claims: claims,
                notBefore: dateTime,
                expires: dateTime.Add(authorizationRequirement.Expiration),
                signingCredentials: authorizationRequirement.SigningCredentials
            );
            // 生成JwtToken
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // 创建JWT令牌生成模型
            var responseJson = new JwtTokenInfo
            {
                token = jwtToken,
                expiration_time = authorizationRequirement.Expiration.TotalSeconds,
                token_type = "Bearer"
            };

            return responseJson;
        }

        /// <summary>
        /// 解析JwtToken字符串
        /// </summary>
        /// <param name="jwtToken">JwtToken字符串</param>
        /// <returns>令牌模型</returns>
        public static JwtTokenData SerializeJwtToken(string jwtToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = jwtHandler.ReadJwtToken(jwtToken);

            object name;
            object role;
            object userData;
            object expiration;
            try
            {
                jwtSecurityToken.Payload.TryGetValue(ClaimTypes.Name, out name);
                jwtSecurityToken.Payload.TryGetValue(ClaimTypes.UserData, out userData);
                jwtSecurityToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtSecurityToken.Payload.TryGetValue(ClaimTypes.Expiration, out expiration);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var jwtTokenData = new JwtTokenData
            {
                UserId = jwtSecurityToken.Id.ToString(),
                UserName = name.IsNullOrEmpty()?"":name.ToString(),
                Role = role.IsNullOrEmpty() ? "" : role.ToString(),
                UserData = userData.IsNullOrEmpty() ? "" : userData.ToString(),
                Expiration = expiration.IsNullOrEmpty() ? DateTime.MinValue : expiration.ToString().ToDateTime(),
            };
            return jwtTokenData;
        }

    }

    /// <summary>
    /// JWT令牌生成模型
    /// </summary>
    public class JwtTokenInfo
    {
        /// <summary>
        /// Token令牌
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 过期时间（秒）
        /// </summary>
        public double expiration_time { get; set; }

        /// <summary>
        /// 令牌类型
        /// </summary>
        public string token_type { get; set; }
    }

    /// <summary>
    /// JWT令牌数据模型
    /// </summary>
    public class JwtTokenData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public string UserData { get; set; }

        /// <summary>
        /// 过期时间（秒）
        /// </summary>
        public DateTime? Expiration { get; internal set; }

        /// <summary>
        /// 角色（多个角色 , 隔开）
        /// </summary>
        public string Role { get; set; }

    }
}
