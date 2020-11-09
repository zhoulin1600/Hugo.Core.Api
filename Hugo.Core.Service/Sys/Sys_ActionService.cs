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
    /// 服务实现：系统功能信息
    /// </summary>
    public class Sys_ActionService : Base.BaseService<DataModel.Sys_Action>, ISys_ActionService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_ActionRepository _repository;

        public Sys_ActionService(
            ILogger<Sys_ActionService> logger,
            IMapper mapper,
            ISys_ActionRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 添加系统功能
        /// </summary>
        /// <param name="model">系统功能信息</param>
        /// <returns></returns>
        public async Task<bool> Add(Sys_Action_Add model)
        {
            var action = model.ChangeType<Sys_Action>();
            action.Id = Guid.NewGuid().ToPrimaryKey();
            return await _repository.Insert(action);
        }

        /// <summary>
        /// 编辑系统功能
        /// </summary>
        /// <param name="model">系统功能信息</param>
        /// <returns></returns>
        public async Task<bool> Edit(Sys_Action_Edit model)
        {
            //return await _repository.Update(model.ChangeType<Sys_Action>());

            return await _repository.Update(c => new Sys_Action
            {
                MenuId = model.MenuId,
                ActionName = model.ActionName,
                ApiUrl = model.ApiUrl,
                Icon = model.Icon,
                Description = model.Description,
                Sort = model.Sort
            }, c => c.Id == model.Id);
        }

        /// <summary>
        /// 删除指定id系统功能信息
        /// </summary>
        /// <param name="id">系统功能id</param>
        /// <returns></returns>
        [SqlSugarTransaction]
        public async Task<bool> Remove(string id)
        {
            //return await _repository.Delete(id);

            return await _repository.Update(c => new Sys_Action { IsDeleted = true }, c => c.Id == id);
        }

        /// <summary>
        /// 获取指定id系统功能信息
        /// </summary>
        /// <param name="id">系统功能id</param>
        /// <returns></returns>
        [Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_Action> Get(string id)
        {
            return await _repository.QueryById<View_Sys_Action>(id, 60);
        }

        /// <summary>
        /// 获取分页系统功能信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_Action>> GetPageList(PageRequest<View_Sys_Action> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_Action>()
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Id), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.MenuId), c => c.MenuId.Equals(pageRequest.Search.MenuId))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.ActionName), c => c.ActionName.Equals(pageRequest.Search.ActionName))

                .And(c => c.IsDeleted.Equals(pageRequest.Search.IsDeleted))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));

            var orderFileds = $"{pageRequest.SortField ?? "Sort" } {pageRequest.SortType ?? "ASC"}";

            return await _repository.QueryPageList<View_Sys_Action>(whereExpression, orderFileds, pageRequest.PageIndex, pageRequest.PageSize);
        }

    }
}