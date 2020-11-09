using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hugo.Core.Common.Swagger
{
    /// <summary>
    /// Swagger服务注入扩展
    /// <para>NuGet：Install-Package Swashbuckle.AspNetCore</para>
    /// <para>NuGet：Install-Package Swashbuckle.AspNetCore.Filters</para>
    /// </summary>
    public static class SwaggerService
    {
        /// <summary>
        /// Swagger服务注入
        /// </summary>
        /// <param name="services">服务容器</param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var ApiName = AppSettings.GetSetting(new string[] { "ApiName" }); //"Hugo.Core.WebApi";
            services.AddSwaggerGen(setupAction =>
            {
                // 遍历出全部的版本，做文档信息展示
                typeof(CustomApiVersion.ApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    // Swagger文档配置
                    setupAction.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{ApiName} — 接口文档",
                        Description = $"{ApiName} HTTP API {version} — {RuntimeInformation.FrameworkDescription}",
                        // 服务条款
                        TermsOfService = new Uri("http://localhost:5000"),
                        // 许可证
                        License = new OpenApiLicense { Name = ApiName, Url = new Uri("http://localhost:5000"), Extensions = { } },
                        // 联系人
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Hugo-Email", Url = new Uri("https://mail.qq.com/"), Email = "45185908@qq.com", Extensions = { } }
                    });
                    // 接口排序
                    setupAction.OrderActionsBy(sortKeySelector => sortKeySelector.RelativePath);
                    // 解决冲突操作
                    setupAction.ResolveConflictingActions(resolver => resolver.FirstOrDefault());

                });

                try
                {
                    //// 获取WebApi的xml注释文件路径
                    //var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{ApiName}.xml");//项目生成XML文档文件名称
                    //setupAction.IncludeXmlComments(xmlPath, true);// 添加WebApi的xml注释说明（Controller的注释）

                    //// 获取DataView的xml注释文件路径
                    //var xmlDataViewPath = Path.Combine(AppContext.BaseDirectory, "Hugo.Core.DataView.xml");
                    //// 添加DataView的xml注释说明（DataView的注释）
                    //setupAction.IncludeXmlComments(xmlDataViewPath);

                    //// 获取DataModel的xml注释文件路径
                    //var xmlDataModelPath = Path.Combine(AppContext.BaseDirectory, "Hugo.Core.DataModel.xml");
                    //// 添加DataDTO的xml注释说明（DataModel的注释）
                    //setupAction.IncludeXmlComments(xmlDataModelPath);


                    // 获取并添加所有Swagger文档xml注释文件  获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                    var basePath = AppContext.BaseDirectory;// Path.GetDirectoryName(typeof(Program).Assembly.Location);
                    var xmls = Directory.GetFiles(basePath, "*.xml");
                    xmls.ForEach(aXml =>
                    {
                        setupAction.IncludeXmlComments(aXml, true);
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception("未找到Swagger所需的API文档注释文件，请检查并拷贝该文件至运行目录。", ex);
                }

                // 配置自定义请求参数过滤器
                setupAction.OperationFilter<CustomParameterFilter>();

                // 开启加权小锁
                setupAction.OperationFilter<AddResponseHeadersFilter>();
                setupAction.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 配置安全校验 在Header中添加Token传递到后台
                setupAction.OperationFilter<SecurityRequirementsOperationFilter>();

                #region JWT Bearer 认证授权
                // 授权（安全方案）配置 -- 开启oauth2安全方案，必须指定oauth2
                setupAction.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Scheme = "oauth2",//HTTP授权方案的名称
                    Name = "Authorization",//参数名称（JWT默认：Authorization）
                    In = ParameterLocation.Header,//API密钥位置（JWT默认：Header）
                    Type = SecuritySchemeType.ApiKey,//类型
                    BearerFormat = "JWT",//令牌token的生成格式
                    Description = "授权内容: Bearer {token} （注意Bearer与token之间空格）<br/>授权地址: /api/Authorization/Login",//简短描述
                });
                #endregion


            });
        }
    }
}