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
    /// 服务实现：系统应用信息
    /// </summary>
    public class Sys_ApplicationService : Base.BaseService<DataModel.Sys_Application>, ISys_ApplicationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_ApplicationRepository _repository;

        public Sys_ApplicationService(
            ILogger<Sys_ApplicationService> logger,
            IMapper mapper,
            ISys_ApplicationRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 添加系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        public async Task<bool> Add(Sys_Application_Add model)
        {
            var application = model.ChangeType<Sys_Application>();
            application.Id = Guid.NewGuid().ToPrimaryKey();
            return await _repository.Insert(application);
        }

        /// <summary>
        /// 编辑系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        public async Task<bool> Edit(Sys_Application_Edit model)
        {
            //return await _repository.Update(model.ChangeType<Sys_Application>());

            return await _repository.Update(c => new Sys_Application
            {
                AppId = model.AppId,
                AppName = model.AppName,
                AppSecret = model.AppSecret,
                AppType = model.AppType
            }, c => c.Id == model.Id);
        }

        /// <summary>
        /// 删除指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        [SqlSugarTransaction]
        public async Task<bool> Remove(string id)
        {
            return await _repository.Delete(id);

            //return await _repository.Update(c => c.IsDeleted == true, c => c.Id == id);
        }

        /// <summary>
        /// 获取指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        [Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_Application> Get(string id)
        {
            return await _repository.QueryById<View_Sys_Application>(id, 60);
        }

        /// <summary>
        /// 获取分页系统应用信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_Application>> GetPageList(PageRequest<View_Sys_Application> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_Application>()
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Id), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.AppId), c => c.AppId.Equals(pageRequest.Search.AppId))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.AppName), c => c.AppName.Equals(pageRequest.Search.AppName))

                .AndIF(!Equals(pageRequest.Search.AppType, null), c => c.AppType.Equals(pageRequest.Search.AppType))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));

            var orderFileds = $"{pageRequest.SortField ?? "CreateTime" } {pageRequest.SortType ?? "DESC"}";

            return await _repository.QueryPageList<View_Sys_Application>(whereExpression, orderFileds, pageRequest.PageIndex, pageRequest.PageSize);
        }

    }
}