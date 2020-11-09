using System.Collections.Generic;
using System.IO;
using Hugo.Core.Common;
using Hugo.Core.Common.AutoGenerate;
using Hugo.Core.Common.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SqlSugar;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// Api控制器：自动生成框架代码文件
    /// </summary>
    [AllowAnonymous]
    public class AutoCodeFileGenerationController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISqlSugarClient _sqlSugarClient;
        public AutoCodeFileGenerationController(IWebHostEnvironment webHostEnvironment, ISqlSugarClient sqlSugarClient)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult ATEST([FromBody] CodeFileModel codeFileModel)
        {
            var path = Path.GetFullPath("..\\Hugo.Core.DataModel");

            if (_webHostEnvironment.IsDevelopment())
            {
                Success("当前为开发环境");
            }
            else
            { 
                Error("当前非开发环境，无法使用此功能");
            }
            return Success(path);
        }

        /// <summary>
        /// 创建整体框架文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <param name="datamodelOpen">实体数据层开关</param>
        /// <param name="viewmodelOpen">视图数据层开关</param>
        /// <param name="repositoryOpen">仓储层开关</param>
        /// <param name="serviceOpen">服务层开关</param>
        /// <param name="controllerOpen">控制器开关</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_AutoFrame_Files([FromBody] CodeFileModel codeFileModel, [FromQuery] bool datamodelOpen, bool viewmodelOpen, bool repositoryOpen, bool serviceOpen, bool controllerOpen)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            var success = new List<string>();
            var fail = new List<string>();
            if (datamodelOpen)
            {
                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.DataModel\\");
                codeFileModel.NameSpace = "Hugo.Core.DataModel";
                codeFileModel.InheritanceClass = "Base.BaseDataModel";
                if (AutoCodeFileGeneration.Create_DataModel_Files(_sqlSugarClient, codeFileModel))
                    success.Add("数据实体层文件-生成成功");
                else
                    fail.Add("数据实体层文件-生成失败");
            }
            if (viewmodelOpen)
            {
                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.DataView\\");
                codeFileModel.NameSpace = "Hugo.Core.DataView";
                codeFileModel.InheritanceClass = "Base.BaseDataView";
                if (AutoCodeFileGeneration.Create_DataView_Files(_sqlSugarClient, codeFileModel))
                    success.Add("数据视图层文件-生成成功");
                else
                    fail.Add("数据视图层文件-生成失败");
            }
            if (repositoryOpen)
            {
                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.IRepository\\");
                codeFileModel.NameSpace = "Hugo.Core.IRepository";
                codeFileModel.InheritanceClass = "Base.IBaseRepository";
                if (AutoCodeFileGeneration.Create_IRepository_Files(_sqlSugarClient, codeFileModel))
                    success.Add("仓储接口层文件-生成成功");
                else
                    fail.Add("仓储接口层文件-生成失败");

                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.Repository\\");
                codeFileModel.NameSpace = "Hugo.Core.Repository";
                codeFileModel.InheritanceClass = "Base.BaseRepository";
                if (AutoCodeFileGeneration.Create_Repository_Files(_sqlSugarClient, codeFileModel))
                    success.Add("仓储实现层文件-生成成功");
                else
                    fail.Add("仓储实现层文件-生成失败");
            }
            if (serviceOpen)
            {
                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.IService\\");
                codeFileModel.NameSpace = "Hugo.Core.IService";
                codeFileModel.InheritanceClass = "Base.IBaseService";
                if (AutoCodeFileGeneration.Create_IService_Files(_sqlSugarClient, codeFileModel))
                    success.Add("服务接口层文件-生成成功");
                else
                    fail.Add("服务接口层文件-生成失败");

                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.Service\\");
                codeFileModel.NameSpace = "Hugo.Core.Service";
                codeFileModel.InheritanceClass = "Base.BaseService";
                if (AutoCodeFileGeneration.Create_Service_Files(_sqlSugarClient, codeFileModel))
                    success.Add("服务实现层文件-生成成功");
                else
                    fail.Add("服务实现层文件-生成失败");
            }
            if (controllerOpen)
            {
                codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.WebApi\\Controllers\\");
                codeFileModel.NameSpace = "Hugo.Core.WebApi.Controllers";
                codeFileModel.InheritanceClass = "BaseController";
                if (AutoCodeFileGeneration.Create_Controller_Files(_sqlSugarClient, codeFileModel))
                    success.Add("Api控制器层文件-生成成功");
                else
                    fail.Add("Api控制器层文件-生成失败");
            }

            return Success(new { success, fail });
        }

        /// <summary>
        /// 创建数据实体层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_DataModel_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if(AutoCodeFileGeneration.Create_DataModel_Files(_sqlSugarClient, codeFileModel))
                return Success("数据实体层文件-生成成功");
            else
                return Error("数据实体层文件-生成失败");
        }

        /// <summary>
        /// 创建数据视图层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_DataView_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_DataView_Files(_sqlSugarClient, codeFileModel))
                return Success("数据视图层文件-生成成功");
            else
                return Error("数据视图层文件-生成失败");
        }

        /// <summary>
        /// 创建仓储接口层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_IRepository_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_IRepository_Files(_sqlSugarClient, codeFileModel))
                return Success("仓储接口层文件-生成成功");
            else
                return Error("仓储接口层文件-生成失败");
        }

        /// <summary>
        /// 创建仓储实现层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Repository_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_Repository_Files(_sqlSugarClient, codeFileModel))
                return Success("仓储实现层文件-生成成功");
            else
                return Error("仓储实现层文件-生成失败");
        }

        /// <summary>
        /// 创建服务接口层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_IService_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_IService_Files(_sqlSugarClient, codeFileModel))
                return Success("服务接口层文件-生成成功");
            else
                return Error("服务接口层文件-生成失败");
        }

        /// <summary>
        /// 创建服务实现层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Service_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_Service_Files(_sqlSugarClient, codeFileModel))
                return Success("服务实现层文件-生成成功");
            else
                return Error("服务实现层文件-生成失败");
        }

        /// <summary>
        /// 创建Api控制器层文件
        /// </summary>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Controller_Files([FromBody] CodeFileModel codeFileModel)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            if (AutoCodeFileGeneration.Create_Controller_Files(_sqlSugarClient, codeFileModel))
                return Success("Api控制器层文件-生成成功");
            else
                return Error("Api控制器层文件-生成失败");
        }

        /*弃用

        /// <summary>
        /// 创建数据实体层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_DataModel_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.DataModel\\");
            string nameSpaceStr = "Hugo.Core.DataModel";
            string baseClass = "Base.BaseDataModel";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"{using}
namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    [SugarTable(""{ClassName}"")]
    public class {ClassName}" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass)) + @"
    {
        public {ClassName}()
        {{Constructor}
        }
        {PropertyName}
    }
}")
                .SettingNamespaceTemplate(template => { return template; })
                .SettingClassDescriptionTemplate(template => 
                template =
@"/// <summary>
    /// {ClassDescription}    /// </summary>")
                .SettingConstructorTemplate(template => template =
@"
            this.{PropertyName} = {DefaultValue};")
                .SettingPropertyDescriptionTemplate(template => template =
@"
        /// <summary>
        /// <para>属性描述：{PropertyDescription}</para>
        /// <para>默认数据：{DefaultValue}</para>
        /// <para>是否可空：{IsNullable}</para>
        /// </summary>")
                .SettingPropertyTemplate(template => template =
@"{SugarColumn}
        public {PropertyType} {PropertyName} { get; set; }")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("{0}", item.Key)}.cs";
                var secondPath = fileName.Split("_").Length > 1 ? fileName.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成数据实体层文件成功");
        }

        /// <summary>
        /// 创建数据视图层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_DataView_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.DataView\\");
            string nameSpaceStr = "Hugo.Core.DataView";
            string baseClass = "Base.BaseDataView";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"{using}
namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public partial class View_{ClassName}" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass)) + @"
    {
        public View_{ClassName}()
        {{Constructor}
        }
        {PropertyName}
    }
}")
                .SettingNamespaceTemplate(template => { return template; })
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// {ClassDescription}    /// </summary>")
                .SettingConstructorTemplate(template => template =
@"
            this.{PropertyName} = {DefaultValue};")
                .SettingPropertyDescriptionTemplate(template => template =
@"
        /// <summary>
        /// <para>属性描述：{PropertyDescription}</para>
        /// <para>默认数据：{DefaultValue}</para>
        /// <para>是否可空：{IsNullable}</para>
        /// </summary>")
                .SettingPropertyTemplate(template => template =
@"{SugarColumn}
        public {PropertyType} {PropertyName} { get; set; }")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("View_{0}", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成数据视图层文件成功");
        }

        /// <summary>
        /// 创建仓储接口层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_IRepository_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.IRepository\\");
            string nameSpaceStr = "Hugo.Core.IRepository";
            string baseClass = "Base.IBaseRepository";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"using Hugo.Core.IRepository.Base;
using Hugo.Core.DataModel;

namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public interface I{ClassName}Repository" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass+ "<DataModel.{ClassName}>")) + @"
    {
        
    }
}")
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// 仓储接口：{ClassDescription}    /// </summary>")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("I{0}Repository", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成仓储接口层文件成功");
        }

        /// <summary>
        /// 创建仓储实现层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Repository_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.Repository\\");
            string nameSpaceStr = "Hugo.Core.Repository";
            string baseClass = "Base.BaseRepository";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public class {ClassName}Repository" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass + "<DataModel.{ClassName}>, I{ClassName}Repository")) + @"
    {
        public {ClassName}Repository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}")
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// 仓储实现：{ClassDescription}    /// </summary>")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("{0}Repository", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成仓储实现层文件成功");
        }

        /// <summary>
        /// 创建服务接口层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_IService_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.IService\\");
            string nameSpaceStr = "Hugo.Core.IService";
            string baseClass = "Base.IBaseService";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public interface I{ClassName}Service" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass + "<DataModel.{ClassName}>")) + @"
    {
        
    }
}")
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// 服务接口：{ClassDescription}    /// </summary>")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("I{0}Service", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成服务接口层文件成功");
        }

        /// <summary>
        /// 创建服务实现层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Service_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.Service\\");
            string nameSpaceStr = "Hugo.Core.Service";
            string baseClass = "Base.BaseService";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"using AutoMapper;
using Hugo.Core.Common.Filter;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IRepository;
using Hugo.Core.IService;
using Hugo.Core.Service.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public class {ClassName}Service" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass + "<DataModel.{ClassName}>, I{ClassName}Service")) + @"
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly I{ClassName}Repository _repository;

        public {ClassName}Service(
            ILogger<{ClassName}Service> logger,
            IMapper mapper,
            I{ClassName}Repository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }
        
    }
}")
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// 服务实现：{ClassDescription}    /// </summary>")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("{0}Service", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成服务实现层文件成功");
        }

        /// <summary>
        /// 创建Api控制器层文件
        /// </summary>
        /// <param name="tableNames">数据库表名（空则全部）</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Create_Controller_Files([FromBody] string[] tableNames)
        {
            if (!_webHostEnvironment.IsDevelopment())
                return Error("当前非开发环境，无法使用此功能");

            string basePath = Path.GetFullPath("..\\Hugo.Core.WebApi\\Controllers\\");
            string nameSpaceStr = "Hugo.Core.WebApi.Controllers";
            string baseClass = "BaseController";
            var dbFirst = _sqlSugarClient.DbFirst;

            if (tableNames != null && tableNames.Length > 0)
                dbFirst = dbFirst.Where(tableNames);

            var ls = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                .SettingClassTemplate(template => template =
@"using AutoMapper;
using Hugo.Core.Common.Controller;
using Hugo.Core.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace " + nameSpaceStr + @"
{
    {ClassDescription}
    public class {ClassName}Controller" + (string.IsNullOrWhiteSpace(baseClass) ? "" : (" : " + baseClass)) + @"
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly I{ClassName}Service _service;

        public {ClassName}Controller(
            ILogger<{ClassName}Controller> logger,
            IMapper mapper,
            I{ClassName}Service service
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._service = service;
        }
        
    }
}")
                .SettingClassDescriptionTemplate(template =>
                template =
@"/// <summary>
    /// Api控制器：{ClassDescription}    /// </summary>")
                .ToClassStringList(nameSpaceStr);

            foreach (var item in ls)
            {
                var fileName = $"{string.Format("{0}Controller", item.Key)}.cs";
                var secondPath = item.Key.Split("_").Length > 1 ? item.Key.Split("_")[0] + "\\" : "";
                var fileFullPath = Path.Combine(basePath + secondPath, fileName);
                if (!Directory.Exists(basePath + secondPath))
                    Directory.CreateDirectory(basePath + secondPath);
                System.IO.File.WriteAllText(fileFullPath, item.Value);
            }
            return Success("生成Api控制器层文件成功");
        }
        */
    }
}
