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
    /// Api控制器：系统用户信息
    /// </summary>
    [Authorize(Policy = GlobalData.AUTH_MANAGER)]
    public class Sys_UserController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_UserService _service;

        public Sys_UserController(
            ILogger<Sys_UserController> logger,
            IMapper mapper,
            ISys_UserService service
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._service = service;
        }


        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(Sys_User_Add model)
        {
            return await this._service.Add(model);
        }

        /// <summary>
        /// 编辑系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Edit(Sys_User_Edit model)
        {
            return await this._service.Edit(model);
        }

        /// <summary>
        /// 删除指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Remove(string id)
        {
            return await this._service.Remove(id);
        }

        /// <summary>
        /// 获取指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_User> Get(string id)
        {
            //return await _service.QueryById<View_Sys_User>(id, 60);
            return await _service.Get(id);
        }

        /// <summary>
        /// 获取分页系统用户信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<View_Sys_User>> GetPageList([FromBody] PageRequest<View_Sys_User> pageRequest)
        {
            return await _service.GetPageList(pageRequest);
        }

    }
}