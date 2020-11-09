using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统日志信息
    /// </summary>
    public interface ISys_LogService : Base.IBaseService<DataModel.Sys_Log>
    {
        /// <summary>
        /// 获取分页系统日志信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_Log>> GetPageList(PageRequest<View_Sys_Log> pageRequest);
    }
}