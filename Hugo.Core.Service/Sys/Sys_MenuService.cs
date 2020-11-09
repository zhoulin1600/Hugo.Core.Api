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
    /// 服务实现：系统菜单信息
    /// </summary>
    public class Sys_MenuService : Base.BaseService<DataModel.Sys_Menu>, ISys_MenuService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_MenuRepository _repository;

        public Sys_MenuService(
            ILogger<Sys_MenuService> logger,
            IMapper mapper,
            ISys_MenuRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 添加系统菜单
        /// </summary>
        /// <param name="model">系统菜单信息</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.新增, LogContent = "添加系统菜单")]
        public async Task<bool> Add(Sys_Menu_Add model)
        {
            var menu = model.ChangeType<Sys_Menu>();
            menu.Id = Guid.NewGuid().ToPrimaryKey();
            return await _repository.Insert(menu);
        }

        /// <summary>
        /// 编辑系统菜单
        /// </summary>
        /// <param name="model">系统菜单信息</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.编辑, LogContent = "编辑系统菜单")]
        public async Task<bool> Edit(Sys_Menu_Edit model)
        {
            //return await _repository.Update(model.ChangeType<Sys_Menu>());

            return await _repository.Update(c => new Sys_Menu
            {
                ParentId = model.ParentId,
                MenuName = model.MenuName,
                Component = model.Component,
                LinkUrl = model.LinkUrl,
                Icon = model.Icon,
                IsHidden = model.IsHidden,
                Description = model.Description,
                Sort = model.Sort
            }, c => c.Id == model.Id);
        }

        /// <summary>
        /// 删除指定id系统菜单信息
        /// </summary>
        /// <param name="id">系统菜单id</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.删除, LogContent = "删除系统菜单")]
        [SqlSugarTransaction]
        public async Task<bool> Remove(string id)
        {
            //return await _repository.Delete(id);

            return await _repository.Update(c => new Sys_Menu { IsDeleted = true }, c => c.Id == id);
        }

        /// <summary>
        /// 获取指定id系统菜单信息
        /// </summary>
        /// <param name="id">系统菜单id</param>
        /// <returns></returns>
        [Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_Menu> Get(string id)
        {
            return await _repository.QueryById<View_Sys_Menu>(id, 60);
        }

        /// <summary>
        /// 获取分页系统菜单信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_Menu>> GetPageList(PageRequest<View_Sys_Menu> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_Menu>()
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Id), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.ParentId), c => c.ParentId.Equals(pageRequest.Search.ParentId))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.MenuName), c => c.MenuName.Equals(pageRequest.Search.MenuName))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Component), c => c.Component.Equals(pageRequest.Search.Component))

                .AndIF(!Equals(pageRequest.Search.IsHidden, null), c => c.IsHidden.Equals(pageRequest.Search.IsHidden))

                .And(c => c.IsDeleted.Equals(pageRequest.Search.IsDeleted))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));

            var orderFileds = $"{pageRequest.SortField ?? "Sort" } {pageRequest.SortType ?? "ASC"}";

            return await _repository.QueryPageList<View_Sys_Menu>(whereExpression, orderFileds, pageRequest.PageIndex, pageRequest.PageSize);
        }

    }
}