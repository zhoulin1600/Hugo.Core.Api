using Hugo.Core.Common;
using Hugo.Core.DataView;
using System.Threading.Tasks;

namespace Hugo.Core.IService
{
    /// <summary>
    /// 服务接口：账户操作
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 管理后台登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        Task<ApiResult> LoginManage(string loginName, string loginPwd);

        /// <summary>
        /// 获取管理后台登录用户数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<View_Sys_User> GetLoginManageData(string userId);
    }
}
