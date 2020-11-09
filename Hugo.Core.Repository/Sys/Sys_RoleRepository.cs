using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统角色信息
    /// </summary>
    public class Sys_RoleRepository : Base.BaseRepository<DataModel.Sys_Role>, ISys_RoleRepository
    {
        public Sys_RoleRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}