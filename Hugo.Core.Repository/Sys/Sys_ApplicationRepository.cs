using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统应用信息
    /// </summary>
    public class Sys_ApplicationRepository : Base.BaseRepository<DataModel.Sys_Application>, ISys_ApplicationRepository
    {
        public Sys_ApplicationRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}