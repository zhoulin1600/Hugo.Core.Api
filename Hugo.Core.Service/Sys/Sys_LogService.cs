using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IRepository;
using Hugo.Core.IService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hugo.Core.Service
{
    /// <summary>
    /// 服务实现：系统日志信息
    /// </summary>
    public class Sys_LogService : Base.BaseService<DataModel.Sys_Log>, ISys_LogService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_LogRepository _repository;

        public Sys_LogService(
            ILogger<Sys_LogService> logger,
            IMapper mapper,
            ISys_LogRepository repository
            ) : base(repository) 
        {
            this._logger = logger;
            this._mapper = mapper;
            this._repository = repository;
        }


        /// <summary>
        /// 获取分页系统日志信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        public async Task<PageResult<View_Sys_Log>> GetPageList(PageRequest<View_Sys_Log> pageRequest)
        {
            var whereExpression = Helper_Linq.True<Sys_Log>()
                .AndIF(!Equals(pageRequest.Search.Id, (long)0), c => c.Id.Equals(pageRequest.Search.Id))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.IP), c => c.IP.Equals(pageRequest.Search.IP))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.OperatorId), c => c.OperatorId.Equals(pageRequest.Search.OperatorId))
                .AndIF(!string.IsNullOrWhiteSpace(pageRequest.Search.OperatorName), c => c.OperatorName.Equals(pageRequest.Search.OperatorName))

                .AndIF(!Equals(pageRequest.Search.LogType, 0), c => c.LogType.Equals(pageRequest.Search.LogType))

                .AndIF(!Equals(pageRequest.BeginDateTime, null), c => c.CreateTime >= pageRequest.BeginDateTime.Value)
                .AndIF(!Equals(pageRequest.EndDateTime, null), c => c.CreateTime < pageRequest.EndDateTime.Value.AddDays(1));
            //var orderFileds = $"{pageRequest.SortField ?? "Sort" } {pageRequest.SortType ?? "ASC"}";

            var result = await _repository.QueryPageList<View_Sys_Log>(whereExpression, orderExpression: c => c.CreateTime, isAsc: false, pageRequest.PageIndex, pageRequest.PageSize);
            result.PageData.ForEach(c => { c.LogTypeName = ((Common.Filter.OperationLogType)c.LogType).GetDescription(); });
            return result;
        }

    }
}