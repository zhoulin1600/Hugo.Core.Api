using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Filter;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IRepository;
using Hugo.Core.IService;
using Hugo.Core.Service.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hugo.Core.Service
{
    /// <summary>
    /// 服务实现：系统角色信息
    /// </summary>
    public class Sys_RoleService : Base.BaseService<DataModel.Sys_Role>, ISys_RoleService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_RoleRepository _repository;

        public Sys_RoleService(
            ILogger<Sys_RoleService> logger,
            IMapper mapper,
            ISys_RoleRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 添加系统角色
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.新增, LogContent = "添加系统角色")]
        public async Task<bool> Add(Sys_Role_Add model)
        {
            var role = model.ChangeType<Sys_Role>();
            role.Id = Guid.NewGuid().ToPrimaryKey();
            return await _repository.Insert(role);
        }

        /// <summary>
        /// 编辑系统角色
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.编辑, LogContent = "编辑系统角色")]
        public async Task<bool> Edit(Sys_Role_Edit model)
        {
            //return await _repository.Update(model.ChangeType<Sys_Role>());

            return await _repository.Update(c => new Sys_Role
            {
                RoleName = model.RoleName,
                Status = model.Status,
                Description = model.Description,
                Sort = model.Sort
            }, c => c.Id == model.Id);
        }

        /// <summary>
        /// 删除指定id系统角色信息
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.删除, LogContent = "删除系统角色")]
        [SqlSugarTransaction]
        public async Task<bool> Remove(string id)
        {
            //return await _repository.Delete(id);

            return await _repository.Update(c => new Sys_Role { IsDeleted = true }, c => c.Id == id);
        }

        /// <summary>
        /// 获取指定id系统角色信息
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        [Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_Role> Get(string id)
        {
            return await _repository.QueryById<View_Sys_Role>(id, 60);
        }

        /// <summary>
        /// 获取分页系统角色信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_Role>> GetPageList(PageRequest<View_Sys_Role> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_Role>()
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Id), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.RoleName), c => c.RoleName.Equals(pageRequest.Search.RoleName))

                .AndIF(!Equals(pageRequest.Search.Status, 0), c => c.Status.Equals(pageRequest.Search.Status))

                .And(c => c.IsDeleted.Equals(pageRequest.Search.IsDeleted))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));

            var orderFileds = $"{pageRequest.SortField ?? "Sort" } {pageRequest.SortType ?? "ASC"}";

            return await _repository.QueryPageList<View_Sys_Role>(whereExpression, orderFileds, pageRequest.PageIndex, pageRequest.PageSize);
        }

    }
}