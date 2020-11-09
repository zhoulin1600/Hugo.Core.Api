using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统应用信息
    /// </summary>
    public interface ISys_ApplicationService : Base.IBaseService<DataModel.Sys_Application>
    {
        /// <summary>
        /// 添加系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        Task<bool> Add(Sys_Application_Add model);

        /// <summary>
        /// 编辑系统应用
        /// </summary>
        /// <param name="model">系统应用信息</param>
        /// <returns></returns>
        Task<bool> Edit(Sys_Application_Edit model);

        /// <summary>
        /// 删除指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// 获取指定id系统应用信息
        /// </summary>
        /// <param name="id">系统应用id</param>
        /// <returns></returns>
        Task<View_Sys_Application> Get(string id);

        /// <summary>
        /// 获取分页系统应用信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_Application>> GetPageList(PageRequest<View_Sys_Application> pageRequest);

    }
}