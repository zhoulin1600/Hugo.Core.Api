using System.Linq;
using System.Threading.Tasks;
using Hugo.Core.Common;
using Hugo.Core.Common.Auth;
using Hugo.Core.Common.Controller;
using Hugo.Core.Common.Filter;
using Hugo.Core.Common.Http;
using Hugo.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hugo.Core.WebApi.Controllers
{
    /// <summary>
    /// Api控制器：账户操作
    /// </summary>
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly ISys_UserService userService;
        private readonly AuthorizationRequirement authorizationRequirement;
        private readonly IHttpContextUser user;

        public AccountController(IAccountService accountService, ISys_UserService userService, AuthorizationRequirement authorizationRequirement,
            IHttpContextUser user)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.authorizationRequirement = authorizationRequirement;
            this.user = user;
        }

        /// <summary>
        /// 管理后台登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult> LoginManage([FromQuery] string loginName, string loginPwd)
        {
            if(loginName.IsNullOrEmpty()||loginPwd.IsNullOrEmpty())
                return Error("用户名或密码不能为空", 0);



            //loginPwd = loginPwd.ToMD5String16();
            //var userInfo = (await userService.QueryList(user => user.UserName == loginName && user.Password == loginPwd && user.IsDeleted == false)).FirstOrDefault();

            //if (userInfo.IsNullOrEmpty())
            //    return Error("用户名或密码错误", 1);
            //if(userInfo.Status == (int)DataView.Enum.DefaultStatus.禁用)
            //    return Error("用户已被冻结，请联系管理员", 2);


            //// 封装用户数据加入JwtToken
            //JwtTokenData jwtTokenData = new JwtTokenData
            //{
            //    UserId = userInfo.Id,
            //    UserName = userInfo.UserName,
            //    UserData = userInfo.ToJson(),
            //    Role = userInfo.Id.Equals("Admin") ? GlobalData.AUTH_ADMIN : GlobalData.AUTH_MANAGER
            //};

            //var jwtTokenInfo = AccessToken.IssueJwtToken(jwtTokenData, authorizationRequirement);
            //return Success(jwtTokenInfo);

            return await accountService.LoginManage(loginName, loginPwd);
        }

        /// <summary>
        /// 获取管理后台登录用户数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetLoginManageData()
        {
            //// token解析获取等用户信息
            //var tokenInfo = AccessToken.SerializeJwtToken(token);

            var id = user.UserId;
            var name = user.UserName;
            var tokenStr = user.GetToken();

            if (string.IsNullOrEmpty(id))
                return Error("用户未授权", 401);

            var result = await accountService.GetLoginManageData(id);

            if (result == null)
                return Error("获取用户信息失败，请重新登录", 401);
            else
                return Success(result);
        }

    }
}
