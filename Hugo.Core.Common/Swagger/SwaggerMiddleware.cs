using Microsoft.AspNetCore.Builder;
using System;
using System.IO;
using System.Linq;

namespace Hugo.Core.Common.Swagger
{
    /// <summary>
    /// Swagger - API文档中间件
    /// </summary>
    public static class SwaggerMiddleware
    {
        /// <summary>
        /// 启用Swagger - API文档中间件
        /// </summary>
        /// <param name="app">应用构造器</param>
        /// <param name="funcStream">用于检索swagger-ui页面的Stream函数</param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app, Func<Stream> funcStream)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                var ApiName = AppSettings.GetSetting(new string[] { "ApiName" }); //"Hugo.Core.WebApi";
                // 根据版本名称倒序 遍历展示
                typeof(CustomApiVersion.ApiVersion).GetEnumNames().OrderByDescending(keySelector => keySelector).ToList().ForEach(version =>
                {
                    // 配置SwaggerUI
                    setupAction.SwaggerEndpoint($"Swagger/{version}/swagger.json", $"{ApiName} {version}");
                });

                // 将swagger设置成首页 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，需要把launchSettings.json中launchUrl去掉
                setupAction.RoutePrefix = string.Empty;
                // 设置为 -1 可不显示models
                //setupAction.DefaultModelsExpandDepth(-1);
                // 设置为 none 可折叠所有方法
                setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                // 设置首页 - 获取或设置用于检索swagger-ui页面的Stream函数
                setupAction.IndexStream = funcStream;

            });
        }
    }
}