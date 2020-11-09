using Hugo.Core.Common;
using Hugo.Core.DataModel;
using Hugo.Core.DataView;
using Hugo.Core.DataView.DTO;
using Hugo.Core.IService.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：系统用户信息
    /// </summary>
    public interface ISys_UserService : Base.IBaseService<DataModel.Sys_User>
    {
        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        Task<bool> Add(Sys_User_Add model);

        /// <summary>
        /// 编辑系统用户
        /// </summary>
        /// <param name="model">系统用户信息</param>
        /// <returns></returns>
        Task<bool> Edit(Sys_User_Edit model);

        /// <summary>
        /// 删除指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// 获取指定id系统用户信息
        /// </summary>
        /// <param name="id">系统用户id</param>
        /// <returns></returns>
        Task<View_Sys_User> Get(string id);

        /// <summary>
        /// 获取分页系统用户信息列表
        /// </summary>
        /// <param name="pageRequest">分页查询模型</param>
        /// <returns></returns>
        Task<PageResult<View_Sys_User>> GetPageList(PageRequest<View_Sys_User> pageRequest);

    }
}