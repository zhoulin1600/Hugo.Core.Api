using AutoMapper;
using Hugo.Core.Common.Filter;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IRepository;
using Hugo.Core.IService;
using Hugo.Core.Service.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hugo.Core.Service
{
    /// <summary>
    /// 服务实现：用户角色信息
    /// </summary>
    public class Sys_UserRoleService : Base.BaseService<DataModel.Sys_UserRole>, ISys_UserRoleService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_UserRoleRepository _repository;

        public Sys_UserRoleService(
            ILogger<Sys_UserRoleService> logger,
            IMapper mapper,
            ISys_UserRoleRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }
        
    }
}