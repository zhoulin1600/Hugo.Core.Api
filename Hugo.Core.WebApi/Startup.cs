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
            // ע����־����Log4Net��
            services.AddSingleton<ILog4Logger, Log4Logger>();

            // ע��IP��������AspNetCoreRateLimit��
            services.AddIPRateLimitService(Configuration);

            // ע����Ӧѹ����ResponseCompression��
            services.AddResponseCompression();

            // ע��API�ĵ�����Swagger��
            services.AddSwaggerService();

            // ע�������Է���Cors��
            services.AddCorsService();

            // ע�����ܷ�������MiniProfiler��
            services.AddMiniProfilerService();

            // ע��ʵʱ���ͷ���SignalR��
            services.AddSignalRService();

            // ע��ӳ�����AutoMapper��
            services.AddAutoMapperService();

            // ע��ORM����SqlSugarCore��
            services.AddSqlSugarClient();

            // ע��Memory�������MemoryCache��
            services.AddMemoryCacheService();

            // ע��Redis�������CSRedisCore��
            services.AddCSRedisCacheService();

            // ע��Redis��Ϣ�����м�����������ߣ���InitQ��
            services.AddRedisMQService(new MessageQueue.RedisSubscribe());

            // ע��HttpContext��ط���
            services.AddHttpContextService();

            // ע����Ȩ����Authorization��
            services.AddAuthorizationService();

            // ע����֤����Authentication������JWTȨ����֤
            services.AddAuthenticationJWTService();

            // �ر�Ĭ�ϵĲ�����֤��ʽ
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddControllers(options =>
            {
                // ����Զ��������֤��ʽ
                options.Filters.Add<ValidationFilterAttribute>();
                // ���ȫ���쳣������
                options.Filters.Add<GlobalExceptionFilter>();
            })
            // NuGet��Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            .AddNewtonsoftJson(setupAction=> {
                // ���ݸ�ʽ��ԭ����� ��ʹ���շ���ʽ��key
                setupAction.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // ����ѭ������
                setupAction.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // ���Կ�ֵ
                setupAction.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // ����ʱ���ʽ
                setupAction.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // ���ݸ�ʽ����ĸСд
                //setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // ��������
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ���Autofac�������� ע����Program.CreateHostBuilder�����Autofac�����ṩ����.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            builder.RegisterModule<AutofacContainer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ����IP�������񣬾����Źܵ����
            app.UseIPRateLimitMiddleware();//app.UseIpRateLimiting();

            // ������Ӧѹ��
            app.UseResponseCompression();

            // �����������Ӧ������־��¼
            app.UseReuestResponseMiddleware();

            // ����IP�������ݼ�¼�м��
            app.UseIPLogMiddleware();

            ////����body����
            //app.Use(next => context =>
            //{
            //    context.Request.EnableBuffering();
            //    return next(context);
            //})

            // ���ÿ��������쳣ҳ��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ����ȫ���쳣�����м��
            app.UseGlobalExceptionMiddleware();

            // ����API�ĵ��м����Swagger��
            app.UseSwaggerMiddleware(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Hugo.Core.WebApi.index.html"));

            // ����Cors�������
            app.UseCors(AppSettings.GetSetting("Cors", "PolicyName"));

            // ����Https�ض���
            //app.UseHttpsRedirection();

            // ���þ�̬�ļ�
            app.UseStaticFiles();

            // ����·��
            app.UseRouting();

            // ������֤�м�� ע��˳��UseRouting -> UseAuthentication -> UseAuthorization��
            app.UseAuthentication();

            // ������Ȩ�м��
            app.UseAuthorization();

            // �������ܷ�����
            app.UseMiniProfiler();

            // �����ս��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // ȫ��·��
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "api/{controller=Home}/{action=Index}/{id?}");

                // SignalRʵʱ���ͷ���·��
                endpoints.MapHub<ChatHub>("/api2/chatHub").RequireCors(t => t.WithOrigins(new string[] { "null" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }
    }
}
