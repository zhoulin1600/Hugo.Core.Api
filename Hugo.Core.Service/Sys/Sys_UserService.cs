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
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hugo.Core.Service
{
    /// <summary>
    /// 服务实现：系统用户信息
    /// </summary>
    public class Sys_UserService : Base.BaseService<DataModel.Sys_User>, ISys_UserService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_UserRepository _repository;

        public Sys_UserService(
            ILogger<Sys_UserService> logger,
            IMapper mapper,
            ISys_UserRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        public async Task<bool> Add(Sys_User_Add model)
        {
            var user = model.ChangeType<Sys_User>();
            user.Id = Guid.NewGuid().ToPrimaryKey();
            user.Password = model.Password.ToMD5String16();
            return await _repository.Insert(user);
        }

        /// <summary>
        /// 编辑系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        public async Task<bool> Edit(Sys_User_Edit model)
        {
            //return await _repository.Update(model.ChangeType<Sys_User>());

            return await _repository.Update(c => new Sys_User
            {
                Status = model.Status,
                Type = model.Type,
                Phone = model.Phone,
                RealName = model.RealName,
                NickName = model.NickName,
                HeadImage = model.HeadImage,
                Remark = model.Remark
            }, c => c.Id == model.Id);
        }

        /// <summary>
        /// 删除指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        [SqlSugarTransaction]
        public async Task<bool> Remove(string id)
        {
            //return await _repository.Delete(id);

            return await _repository.Update(c => new Sys_User { IsDeleted = true }, c => c.Id == id);
        }

        /// <summary>
        /// 获取指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        [Hugo.Core.Common.Filter.Caching(AbsoluteExpiration = 1)]
        public async Task<View_Sys_User> Get(string id)
        {
            return await _repository.QueryById<View_Sys_User>(id, 60);
        }

        /// <summary>
        /// 获取分页系统用户信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_User>> GetPageList(PageRequest<View_Sys_User> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_User>()
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Id), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.UserName), c => c.UserName.Equals(pageRequest.Search.UserName))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.Phone), c => c.Phone.Equals(pageRequest.Search.Phone))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.NickName), c => c.NickName.Contains(pageRequest.Search.NickName))

                .AndIF(!Equals(pageRequest.Search.Status, 0), c => c.Status.Equals(pageRequest.Search.Status))
                .AndIF(!Equals(pageRequest.Search.Type, 0), c => c.Type.Equals(pageRequest.Search.Type))

                .And(c => c.IsDeleted.Equals(pageRequest.Search.IsDeleted))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));

            var orderFileds = $"{pageRequest.SortField ?? "CreateTime" } {pageRequest.SortType ?? "DESC"}";

            return await _repository.QueryPageList<View_Sys_User>(whereExpression, orderFileds, pageRequest.PageIndex, pageRequest.PageSize);
        }

    }
}