using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统功能信息
    /// </summary>
    public interface ISys_ActionService : Base.IBaseService<DataModel.Sys_Action>
    {
        /// <summary>
        /// 添加系统功能
        /// </summary>
        /// <param name="model">系统功能信息</param>
        /// <returns></returns>
        Task<bool> Add(Sys_Action_Add model);

        /// <summary>
        /// 编辑系统功能
        /// </summary>
        /// <param name="model">系统功能信息</param>
        /// <returns></returns>
        Task<bool> Edit(Sys_Action_Edit model);

        /// <summary>
        /// 删除指定id系统功能信息
        /// </summary>
        /// <param name="id">系统功能id</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// 获取指定id系统功能信息
        /// </summary>
        /// <param name="id">系统功能id</param>
        /// <returns></returns>
        Task<View_Sys_Action> Get(string id);

        /// <summary>
        /// 获取分页系统功能信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_Action>> GetPageList(PageRequest<View_Sys_Action> pageRequest);

    }
}