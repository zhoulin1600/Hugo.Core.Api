using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统用户信息
    /// </summary>
    public class Sys_UserRepository : Base.BaseRepository<DataModel.Sys_User>, ISys_UserRepository
    {
        public Sys_UserRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}