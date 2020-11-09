using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Controller;
using Hugo.Core.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// RestfulApi
    /// </summary>
    [Route("Api/[controller]")]
    public class RestfulApiController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RestfulApiController(
            ILogger<RestfulApiController> logger,
            IMapper mapper
            )
        {
            this._logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// HttpGet（传统Url参数方式）
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("[action]")]
        public ApiResult<string> GetIdInfo([FromQuery] string id)
        {
            return Success("HttpGet（传统Url参数方式）：" + id);
        }

        /// <summary>
        /// HttpGet（Restful方式）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResult<string> GetId([FromRoute] string id)
        {
            return Success("HttpGet（Restful方式）：" + id);
        }

        /// <summary>
        /// HttpGet（无参方式）
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetList()
        {
            return Ok("HttpGet（无参）");
        }

        /// <summary>
        /// HttpDelete（传统Url参数方式）
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        [HttpDelete()]
        public string Delete([FromQuery] string id)
        {
            return "HttpDelete（传统Url参数方式）：" + id;
        }

        /// <summary>
        /// HttpDelete（Restful方式）
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public string Remove([FromRoute] string id)
        {
            return "HttpDelete（Restful方式）：" + id;
        }

        /// <summary>
        /// HttpPost - 新增
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        [HttpPost]
        public object Add([FromBody] Sys_User model)
        {
            return model;
        }

        /// <summary>
        /// HttpPut - 修改
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public object Edit([FromRoute]string id, [FromBody] Sys_User model)
        {
            model.Id = id;
            return model;
        }


    }
}
