using AutoMapper;
using Hugo.Core.Common;
using Hugo.Core.Common.Auth;
using Hugo.Core.Common.Filter;
using Hugo.Core.DataView;
using Hugo.Core.IRepository;
using Hugo.Core.IService;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Hugo.Core.Service
{
    /// <summary>
    /// 服务实现：账户操作
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISys_UserRepository userRepository;
        private readonly AuthorizationRequirement authorizationRequirement;

        public AccountService(
            ILogger<AccountService> logger,
            IMapper mapper,
            ISys_UserRepository userRepository,
            AuthorizationRequirement authorizationRequirement
            )
        {
            this._logger = logger;
            this._mapper = mapper;
            this.userRepository = userRepository;
            this.authorizationRequirement = authorizationRequirement;
        }


        /// <summary>
        /// 管理后台登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        [OperationLog(LogType = OperationLogType.授权登录, LogContent = "管理后台登录")]
        public async Task<ApiResult> LoginManage(string loginName, string loginPwd)
        {
            loginPwd = loginPwd.ToMD5String16();
            var userInfo = (await userRepository.QueryList(user => user.UserName == loginName && user.Password == loginPwd && user.IsDeleted == false)).FirstOrDefault();

            if (userInfo.IsNullOrEmpty())
                return new ApiResult { Success = false, StatusCode = 1, Message = "用户名或密码错误" };
            if (userInfo.Status == (int)DataView.Enum.DefaultStatus.禁用)
                return new ApiResult { Success = false, StatusCode = 2, Message = "用户已被冻结，请联系管理员" };


            // 封装用户数据加入JwtToken
            var jwtTokenData = new JwtTokenData
            {
                UserId = userInfo.Id,
                UserName = userInfo.UserName,
                UserData = userInfo.ToJson(),
                Role = userInfo.Id.Equals("Admin") ? GlobalData.AUTH_ADMIN : GlobalData.AUTH_MANAGER
            };

            var jwtTokenInfo = AccessToken.IssueJwtToken(jwtTokenData, authorizationRequirement);
            return new ApiResult<JwtTokenInfo> { Data = jwtTokenInfo, Message = "授权成功" };
        }

        /// <summary>
        /// 获取管理后台登录用户数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 5)]
        public async Task<View_Sys_User> GetLoginManageData(string userId)
        {

            #region 用户信息
            var userInfo = await userRepository.QueryById<View_Sys_User>(id: userId);
            userInfo.Password = "";
            //userInfo.PayPwd = "";
            #endregion

            #region 菜单信息
            //var memuInfo = new List<DataView.View_Sys_Menu>();
            //string[] memuIds = { };
            //if (userInfo.Id.Equals("Admin"))
            //{
            //    memuIds = (await menuService.QueryList(c => c.MenuType == userInfo.Type)).Select(c => c.Id).ToArray();
            //}
            //else
            //{
            //    var roleIds = (await userRoleService.QueryList(c => c.UserId == userInfo.Id)).Select(c => c.RoleId).ToArray();
            //    memuIds = (await permissionService.QueryList(c => roleIds.Contains(c.RoleId))).Select(c => c.MenuId).ToArray();
            //}
            //memuInfo = await menuService.GetMenuStructure(memuIds);
            #endregion

            return userInfo;
        }
    }
}
