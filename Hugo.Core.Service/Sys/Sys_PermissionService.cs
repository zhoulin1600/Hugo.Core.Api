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
    /// 服务实现：系统权限信息
    /// </summary>
    public class Sys_PermissionService : Base.BaseService<DataModel.Sys_Permission>, ISys_PermissionService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_PermissionRepository _repository;

        public Sys_PermissionService(
            ILogger<Sys_PermissionService> logger,
            IMapper mapper,
            ISys_PermissionRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }
        
    }
}