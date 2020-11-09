using Autofac;
using Hugo.Core.Common;
using Hugo.Core.Common.Auth;
using Hugo.Core.Common.AutofacRegister;
using Hugo.Core.Common.AutoMapperTool;
using Hugo.Core.Common.Cache;
using Hugo.Core.Common.Cors;
using Hugo.Core.Common.Filter;
using Hugo.Core.Common.Http;
using Hugo.Core.Common.IPRateLimit;
using Hugo.Core.Common.Logger;
using Hugo.Core.Common.MessageQueue;
using Hugo.Core.Common.Middleware;
using Hugo.Core.Common.MiniProfilerTool;
using Hugo.Core.Common.ORM;
using Hugo.Core.Common.SignalR;
using Hugo.Core.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Hugo.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册日志服务（Log4Net）
            services.AddSingleton<ILog4Logger, Log4Logger>();

            // 注册IP限流服务（AspNetCoreRateLimit）
            services.AddIPRateLimitService(Configuration);

            // 注册响应压缩（ResponseCompression）
            services.AddResponseCompression();

            // 注册API文档服务（Swagger）
            services.AddSwaggerService();

            // 注册跨域策略服务（Cors）
            services.AddCorsService();

            // 注册性能分析器（MiniProfiler）
            services.AddMiniProfilerService();

            // 注册实时推送服务（SignalR）
            services.AddSignalRService();

            // 注册映射服务（AutoMapper）
            services.AddAutoMapperService();

            // 注册ORM服务（SqlSugarCore）
            services.AddSqlSugarClient();

            // 注册Memory缓存服务（MemoryCache）
            services.AddMemoryCacheService();

            // 注册Redis缓存服务（CSRedisCore）
            services.AddCSRedisCacheService();

            // 注册Redis消息队列中间件服务（消费者）（InitQ）
            services.AddRedisMQService(new MessageQueue.RedisSubscribe());

            // 注册HttpContext相关服务
            services.AddHttpContextService();

            // 注册授权服务（Authorization）
            services.AddAuthorizationService();

            // 注册认证服务（Authentication）基于JWT权限认证
            services.AddAuthenticationJWTService();

            // 关闭默认的参数验证方式
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddControllers(options =>
            {
                // 添加自定义参数验证方式
                options.Filters.Add<ValidationFilterAttribute>();
                // 添加全局异常捕获器
                options.Filters.Add<GlobalExceptionFilter>();
            })
            // NuGet：Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            .AddNewtonsoftJson(setupAction=> {
                // 数据格式按原样输出 不使用驼峰样式的key
                setupAction.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 忽略循环引用
                setupAction.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // 忽略空值
                setupAction.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // 设置时间格式
                setupAction.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 数据格式首字母小写
                //setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // 配置容器
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 添加Autofac服务容器 注意在Program.CreateHostBuilder中添加Autofac服务提供工厂.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            builder.RegisterModule<AutofacContainer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 启用IP限流服务，尽量放管道外层
            app.UseIPRateLimitMiddleware();//app.UseIpRateLimiting();

            // 启用响应压缩
            app.UseResponseCompression();

            // 启用请求和响应数据日志记录
            app.UseReuestResponseMiddleware();

            // 启用IP请求数据记录中间件
            app.UseIPLogMiddleware();

            ////允许body重用
            //app.Use(next => context =>
            //{
            //    context.Request.EnableBuffering();
            //    return next(context);
            //})

            // 启用开发环境异常页面
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 启用全局异常捕获中间件
            app.UseGlobalExceptionMiddleware();

            // 启用API文档中间件（Swagger）
            app.UseSwaggerMiddleware(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Hugo.Core.WebApi.index.html"));

            // 启用Cors跨域策略
            app.UseCors(AppSettings.GetSetting("Cors", "PolicyName"));

            // 启用Https重定向
            //app.UseHttpsRedirection();

            // 启用静态文件
            app.UseStaticFiles();

            // 启用路由
            app.UseRouting();

            // 启用认证中间件 注意顺序（UseRouting -> UseAuthentication -> UseAuthorization）
            app.UseAuthentication();

            // 启用授权中间件
            app.UseAuthorization();

            // 启用性能分析器
            app.UseMiniProfiler();

            // 启用终结点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // 全局路由
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "api/{controller=Home}/{action=Index}/{id?}");

                // SignalR实时推送服务路由
                endpoints.MapHub<ChatHub>("/api2/chatHub").RequireCors(t => t.WithOrigins(new string[] { "null" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }
    }
}
