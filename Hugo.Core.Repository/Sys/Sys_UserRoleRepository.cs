using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：用户角色信息
    /// </summary>
    public class Sys_UserRoleRepository : Base.BaseRepository<DataModel.Sys_UserRole>, ISys_UserRoleRepository
    {
        public Sys_UserRoleRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}