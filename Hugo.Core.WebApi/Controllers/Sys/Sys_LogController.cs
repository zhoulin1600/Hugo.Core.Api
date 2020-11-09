using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Controller;
using Hugo.Core.DataView;
using Hugo.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// Api控制器：系统日志信息
    /// </summary>
    [Authorize(Policy = GlobalData.AUTH_MANAGER)]
    public class Sys_LogController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_LogService _service;

        public Sys_LogController(
            ILogger<Sys_LogController> logger,
            IMapper mapper,
            ISys_LogService service
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._service = service;
        }

        /// <summary>
        /// 获取系统日志类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public ApiResult GetLogType()
        {
            var result = Helper_Enum.ToOptionList(typeof(Common.Filter.OperationLogType));
            return Success(result);
        }

        /// <summary>
        /// 获取分页系统日志信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<View_Sys_Log>> GetPageList([FromBody] PageRequest<View_Sys_Log> pageRequest)
        {
            return await _service.GetPageList(pageRequest);
        }

    }
}