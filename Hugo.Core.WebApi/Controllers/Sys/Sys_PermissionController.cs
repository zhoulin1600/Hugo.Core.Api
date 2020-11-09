using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Controller;
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
    /// Api控制器：系统权限信息
    /// </summary>
    [Authorize(Policy = GlobalData.AUTH_MANAGER)]
    public class Sys_PermissionController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_PermissionService _service;

        public Sys_PermissionController(
            ILogger<Sys_PermissionController> logger,
            IMapper mapper,
            ISys_PermissionService service
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._service = service;
        }
        
    }
}