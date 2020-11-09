using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hugo.Core.Common;
using Hugo.Core.Common.Controller;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// Api控制器：上传操作
    /// </summary>
    public class UploadController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;

        }

        /// <summary>
        /// 获取绝对物理路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns></returns>
        protected string GetAbsolutePath(string virtualPath)
        {
            string path = virtualPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (path[0] == '~')
                path = path.Remove(0, 2);
            string rootPath = _webHostEnvironment.WebRootPath;// HttpContext.RequestServices.GetService<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>().WebRootPath;

            return Path.Combine(rootPath, path);
        }

        /// <summary>
        /// 上传文件（API服务器）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFileByForm()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(Error("上传文件缺失", 404).ToJson());

            string path = $"/Upload/{DateTime.Now.ToShortDateString()}/{file.FileName}";
            string physicPath = GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{_configuration["HostUrl:AipRootUrl"]}{path}";
            var result = Success<object>(new { FileName = file.FileName, ThumbUrl = url, Url = url });
            return JsonContent(result.ToJson());
        }


        /// <summary>
        /// 上传图片（资源服务器）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Upload/Pic")]
        public async Task<IActionResult> UploadImage()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(Error("上传图片缺失", 412).ToJson());

            //格式限制
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };
            if (!allowType.Contains(file.ContentType))
                return JsonContent(Error("图片格式错误", 415).ToJson());

            //大小限制
            if (file.Length > 1024 * 1024 * 2)
                return JsonContent(Error("图片大小超过2M", 413).ToJson());

            var baseUrl = _configuration["HostUrl:UpLoadUrl"];
            var basePath = _configuration["HostUrl:UpLoadPath"];
            var folderPath = DateTime.Now.ToShortDateString();
            var fileName = $"{Guid.NewGuid().ToLongString()}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";//Guid.NewGuid().ToLongString() + file.FileName;
            var fullPath = Path.Combine(basePath, folderPath, fileName);
            string dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fs); //file.CopyTo(fs);
            }
            var url = $"{baseUrl}/{folderPath}/{fileName}";
            var result = Success<object>(new { FileName = fileName, ThumbUrl = url, Url = url }, "上传图片成功");
            return JsonContent(result.ToJson());
        }

        /// <summary>
        /// 上传文件（资源服务器）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Upload/File")]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(Error("上传文件缺失", 412).ToJson());

            ////格式限制
            //var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };
            //if (!allowType.Contains(file.ContentType))
            //    return JsonContent(Error("图片格式错误", 415).ToJson());

            //大小限制
            if (file.Length > 1024 * 1024 * 2)
                return JsonContent(Error("文件大小超过2M", 413).ToJson());

            var baseUrl = _configuration["HostUrl:UpLoadUrl"];
            var basePath = _configuration["HostUrl:UpLoadPath"];
            var folderPath = DateTime.Now.ToShortDateString();
            var fileName = $"{Guid.NewGuid().ToLongString()}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";//Guid.NewGuid().ToLongString() + file.FileName;
            var fullPath = Path.Combine(basePath, folderPath, fileName);
            string dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fs); //file.CopyTo(fs);
            }
            var url = $"{baseUrl}/{folderPath}/{fileName}";
            var result = Success<object>(new { FileName = fileName, ThumbUrl = url, Url = url }, "上传文件成功");
            return JsonContent(result.ToJson());
        }

    }
}
