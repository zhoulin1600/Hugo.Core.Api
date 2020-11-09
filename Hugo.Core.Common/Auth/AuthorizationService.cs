using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hugo.Core.Common.Auth
{
    /// <summary>
    /// 授权服务注入扩展
    /// </summary>
    public static class AuthorizationService
    {
        /// <summary>
        /// 授权服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            #region ★默认授权
            // API层的控制器或方法增加特性 - [Authorize]
            services.AddAuthorization();
            #endregion

            #region ★基于角色授权
            // API层的控制器或方法增加特性 - [Authorize(Roles = "Admin")] - 注意：token的角色配置
            #endregion

            #region ★基于策略授权
            // API层的控制器或方法增加特性 - [Authorize(Policy = "Admin")] - 多个角色混合
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(GlobalData.AUTH_ADMIN, policy => policy.RequireRole(GlobalData.AUTH_ADMIN).Build());
                configure.AddPolicy(GlobalData.AUTH_MANAGER, policy => policy.RequireRole(GlobalData.AUTH_ADMIN, GlobalData.AUTH_MANAGER).Build());
                configure.AddPolicy(GlobalData.AUTH_CLIENT, policy => policy.RequireRole(GlobalData.AUTH_ADMIN, GlobalData.AUTH_MANAGER, GlobalData.AUTH_CLIENT));
            });
            #endregion

            #region ★自定义复杂策略授权
            var issuer = AppSettings.GetSetting(new string[] { "TokenConfig", "Issuer" });
            var audience = AppSettings.GetSetting(new string[] { "TokenConfig", "Audience" });
            var secretKey = AppSettings.GetSetting(new string[] { "TokenConfig", "SecretKey" });
            var expiration = AppSettings.GetSetting(new string[] { "TokenConfig", "Expiration" }).ToInt();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            // 权限详情项 - 动态绑定（处理器里动态赋值）
            var authorities = new List<AuthorityItem>();

            var authorizationRequirement = new AuthorizationRequirement(
                issuer,
                audience,
                expiration: TimeSpan.FromSeconds(expiration),
                signingCredentials,
                ClaimTypes.Role,//基于角色的授权
                authorities
                );

            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(GlobalData.Authority_Policy_Name,
                         policy => policy.Requirements.Add(authorizationRequirement));
            });

            #endregion

            #region ★基于Scope策略授权
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Scope_Module_Policy", builder =>
            //    {
            //        //客户端Scope中包含core.api.Module才能访问
            //        builder.RequireScope("core.api.Module");
            //    });

            //    // 其他 Scope 策略
            //    // ...

            //});
            #endregion

            // 注入授权条件配置信息（已实例化）
            services.AddSingleton(authorizationRequirement);
            //// 这里冗余写了一次,因为很多人看不到
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //// 注入权限处理器
            //services.AddScoped<IAuthorizationHandler, PermissionHandler>();

        }
    }
}
