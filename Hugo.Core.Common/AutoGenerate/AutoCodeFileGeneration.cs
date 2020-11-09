using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hugo.Core.Common.AutoGenerate
{
    /// <summary>
    /// 自动生成框架代码文件
    /// <para>基于SqlSugar-DbFirst</para>
    /// </summary>
    public class AutoCodeFileGeneration
    {

        /// <summary>
        /// 创建数据实体层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_DataModel_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.DataModel\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.DataModel";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.BaseDataModel";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(template => template =
    @"{using}
namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    [SugarTable(""{ClassName}"")]
    public class {ClassName}" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass)) + @"
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
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "{0}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建数据视图层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_DataView_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.DataView\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.DataView";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.BaseDataView";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(template => template =
    @"{using}
namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public partial class View_{ClassName}" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass)) + @"
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
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "View_{0}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建仓储接口层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_IRepository_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.IRepository\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.IRepository";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.IBaseRepository";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(template => template =
    @"using Hugo.Core.IRepository.Base;
using Hugo.Core.DataModel;

namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public interface I{ClassName}Repository" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass + "<DataModel.{ClassName}>")) + @"
    {
        
    }
}")
                    .SettingClassDescriptionTemplate(template =>
                    template =
    @"/// <summary>
    /// 仓储接口：{ClassDescription}    /// </summary>")
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "I{0}Repository");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建仓储实现层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_Repository_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.Repository\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.Repository";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.BaseRepository";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(template => template =
    @"using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public class {ClassName}Repository" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass + "<DataModel.{ClassName}>, I{ClassName}Repository")) + @"
    {
        public {ClassName}Repository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}")
                    .SettingClassDescriptionTemplate(template =>
                    template =
    @"/// <summary>
    /// 仓储实现：{ClassDescription}    /// </summary>")
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "{0}Repository");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建服务接口层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_IService_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.IService\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.IService";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.IBaseService";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
                    .SettingClassTemplate(template => template =
    @"using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public interface I{ClassName}Service" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass + "<DataModel.{ClassName}>")) + @"
    {
        
    }
}")
                    .SettingClassDescriptionTemplate(template =>
                    template =
    @"/// <summary>
    /// 服务接口：{ClassDescription}    /// </summary>")
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "I{0}Service");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建服务实现层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_Service_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.Service\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.Service";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "Base.BaseService";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
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

namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public class {ClassName}Service" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass + "<DataModel.{ClassName}>, I{ClassName}Service")) + @"
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
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "{0}Service");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建Api控制器层文件
        /// </summary>
        /// <param name="sqlSugarClient">SqlSugar上下文</param>
        /// <param name="codeFileModel">代码文件生成模型</param>
        /// <returns></returns>
        public static bool Create_Controller_Files(ISqlSugarClient sqlSugarClient, CodeFileModel codeFileModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codeFileModel.MainFolderPath))
                    codeFileModel.MainFolderPath = Path.GetFullPath("..\\Hugo.Core.WebApi\\Controllers\\");
                if (string.IsNullOrWhiteSpace(codeFileModel.NameSpace))
                    codeFileModel.NameSpace = "Hugo.Core.WebApi.Controllers";
                if (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass))
                    codeFileModel.InheritanceClass = "BaseController";

                var dbFirst = sqlSugarClient.DbFirst;
                if (codeFileModel.TableNames != null && codeFileModel.TableNames.Length > 0)
                    dbFirst = dbFirst.Where(codeFileModel.TableNames);

                var dictList = dbFirst.IsCreateDefaultValue().IsCreateAttribute()
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

namespace " + codeFileModel.NameSpace + @"
{
    {ClassDescription}
    public class {ClassName}Controller" + (string.IsNullOrWhiteSpace(codeFileModel.InheritanceClass) ? "" : (" : " + codeFileModel.InheritanceClass)) + @"
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
                    .ToClassStringList(codeFileModel.NameSpace);

                Create_Files(dictList, codeFileModel.MainFolderPath, codeFileModel.SubPathModel, "{0}Controller");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// 批量文件生成
        /// </summary>
        /// <param name="dictList">模板内容字典集合</param>
        /// <param name="mainFolderPath">文件夹主路径</param>
        /// <param name="subPathModel">文件夹从路径</param>
        /// <param name="fileNameTemp">文件名模板</param>
        private static void Create_Files(Dictionary<string, string> dictList, string mainFolderPath, SubPathModel subPathModel, string fileNameTemp)
        {
            foreach (var dict in dictList)
            {
                var fileName = $"{string.Format(fileNameTemp, dict.Key)}.cs";
                var subFolderPath = string.Empty;
                if (subPathModel != null && subPathModel.PathOpen)
                {
                    if (subPathModel.AutoTable)
                        subFolderPath = fileName.Split("_").Length > 1 ? fileName.Split("_")[0] : string.Empty;
                    else
                        subFolderPath = subPathModel.SubFolderPath;
                }

                var fileFullPath = Path.Combine(mainFolderPath, subFolderPath, fileName);
                if (!Directory.Exists(Path.Combine(mainFolderPath, subFolderPath)))
                    Directory.CreateDirectory(Path.Combine(mainFolderPath, subFolderPath));
                File.WriteAllText(fileFullPath, dict.Value);
            }
        }

    }

    /// <summary>
    /// 代码文件生成模型
    /// </summary>
    public class CodeFileModel
    {
        /// <summary>
        /// 数据库表名（空则全部）
        /// </summary>
        public string[] TableNames { get; set; }

        /// <summary>
        /// 主文件夹路径
        /// </summary>
        public string MainFolderPath { get; set; }

        /// <summary>
        /// 子路径模型
        /// </summary>
        public SubPathModel SubPathModel { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 继承类
        /// </summary>
        public string InheritanceClass { get; set; }

    }

    /// <summary>
    /// 次级路径模型
    /// </summary>
    public class SubPathModel
    {
        /// <summary>
        /// 次级路径开关
        /// </summary>
        public bool PathOpen { get; set; }

        /// <summary>
        /// 自动从表名获取子路径（表名：System_Table，子路径名：System）
        /// </summary>
        public bool AutoTable { get; set; }

        /// <summary>
        /// 子文件夹路径名
        /// </summary>
        public string SubFolderPath { get; set; }

    }

}
