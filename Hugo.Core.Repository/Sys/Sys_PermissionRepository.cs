using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统权限信息
    /// </summary>
    public class Sys_PermissionRepository : Base.BaseRepository<DataModel.Sys_Permission>, ISys_PermissionRepository
    {
        public Sys_PermissionRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}