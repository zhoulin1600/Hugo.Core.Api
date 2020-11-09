using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Controller;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
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
    /// Api控制器：系统应用信息
    /// </summary>
    [Authorize(Policy = GlobalData.AUTH_MANAGER)]
    public class Sys_ApplicationController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_ApplicationService _service;

        public Sys_ApplicationController(
            ILogger<Sys_ApplicationController> logger,
            IMapper mapper,
            ISys_ApplicationService service
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._service = service;
        }


        /// <summary>
        /// 添加系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = GlobalData.AUTH_ADMIN)]
        public async Task<bool> Add(Sys_Application_Add model)
        {
            return await this._service.Add(model);
        }

        /// <summary>
        /// 编辑系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = GlobalData.AUTH_ADMIN)]
        public async Task<bool> Edit(Sys_Application_Edit model)
        {
            return await this._service.Edit(model);
        }

        /// <summary>
        /// 删除指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = GlobalData.AUTH_ADMIN)]
        public async Task<bool> Remove(string id)
        {
            return await this._service.Remove(id);
        }

        /// <summary>
        /// 获取指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_Application> Get(string id)
        {
            //return await _service.QueryById<View_Sys_User>(id, 60);
            return await _service.Get(id);
        }

        /// <summary>
        /// 获取分页系统应用信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<View_Sys_Application>> GetPageList([FromBody] PageRequest<View_Sys_Application> pageRequest)
        {
            return await _service.GetPageList(pageRequest);
        }

    }
}