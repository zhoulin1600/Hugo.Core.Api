using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统角色信息
    /// </summary>
    public interface ISys_RoleService : Base.IBaseService<DataModel.Sys_Role>
    {
        /// <summary>
        /// 添加系统角色
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        Task<bool> Add(Sys_Role_Add model);

        /// <summary>
        /// 编辑系统角色
        /// </summary>
        /// <param name="model">系统角色信息</param>
        /// <returns></returns>
        Task<bool> Edit(Sys_Role_Edit model);

        /// <summary>
        /// 删除指定id系统角色信息
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// 获取指定id系统角色信息
        /// </summary>
        /// <param name="id">系统角色id</param>
        /// <returns></returns>
        Task<View_Sys_Role> Get(string id);

        /// <summary>
        /// 获取分页系统角色信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_Role>> GetPageList(PageRequest<View_Sys_Role> pageRequest);

    }
}