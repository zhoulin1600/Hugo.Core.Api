using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hugo.Core.Common.Auth
{
    /// <summary>
    /// 认证服务注入扩展（基于JWT权限认证）
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.Authentication.JwtBearer</para>
    /// </summary>
    public static class AuthenticationJWTService
    {
        /// <summary>
        /// 认证服务注入（基于JWT权限认证）
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddAuthenticationJWTService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 令牌配置信息
            var issuer = AppSettings.GetSetting(new string[] { "TokenConfig", "Issuer" });
            var audience = AppSettings.GetSetting(new string[] { "TokenConfig", "Audience" });
            var secretKey = AppSettings.GetSetting(new string[] { "TokenConfig", "SecretKey" });
            var expiration = AppSettings.GetSetting(new string[] { "TokenConfig", "Expiration" }).ToInt();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,//是否验证颁发者Issuer
                ValidIssuer = issuer,//颁发者Issuer
                ValidateAudience = true,//是否验证订阅者Audience
                ValidAudience = audience,//订阅者Audience
                ValidateIssuerSigningKey = true,//是否验证颁发者签名密钥IssuerSigningKey
                IssuerSigningKey = signingKey,//颁发者签名密钥IssuerSigningKey
                ValidateLifetime = true,//是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                RequireExpirationTime = true,//是否要求Token的Claims中必须包含Expires过期时间
                ClockSkew = TimeSpan.Zero,//缓冲过期时间（默认5分钟） - （JwtBearerToken过期时间+缓冲过期时间=完整有效时间）//TimeSpan.FromSeconds(30),
            };

            // 启用认证方案（官方JwtBearer认证）
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // 添加JwtBearer认证服务
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.Events = new JwtBearerEvents
                {
                    // （未授权时调用）在将质询发送回呼叫者之前调用
                    OnChallenge = context =>
                    {
                        context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    // （自定义Token获取方式）在首次接收到协议消息时调用
                    OnMessageReceived = context =>
                    {
                        // 通过请求参数获取Token访问令牌
                        context.Token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                        //context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    },
                    // （认证失败时调用）在请求处理期间引发异常时调用 -- 除非抑制，否则此事件之后将重新抛出异常
                    OnAuthenticationFailed = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                        if (jwtToken.Issuer != issuer)
                        {
                            context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                        }

                        if (jwtToken.Audiences.FirstOrDefault() != audience)
                        {
                            context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                        }

                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        }
    }
}
