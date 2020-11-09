using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IService.Base;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统菜单信息
    /// </summary>
    public interface ISys_MenuService : Base.IBaseService<DataModel.Sys_Menu>
    {
        /// <summary>
        /// 添加系统菜单
        /// </summary>
        /// <param name="model">系统菜单信息</param>
        /// <returns></returns>
        Task<bool> Add(Sys_Menu_Add model);

        /// <summary>
        /// 编辑系统菜单
        /// </summary>
        /// <param name="model">系统菜单信息</param>
        /// <returns></returns>
        Task<bool> Edit(Sys_Menu_Edit model);

        /// <summary>
        /// 删除指定id系统菜单信息
        /// </summary>
        /// <param name="id">系统菜单id</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// 获取指定id系统菜单信息
        /// </summary>
        /// <param name="id">系统菜单id</param>
        /// <returns></returns>
        Task<View_Sys_Menu> Get(string id);

        /// <summary>
        /// 获取分页系统菜单信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_Menu>> GetPageList(PageRequest<View_Sys_Menu> pageRequest);

    }
}