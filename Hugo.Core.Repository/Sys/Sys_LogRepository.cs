using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统日志信息
    /// </summary>
    public class Sys_LogRepository : Base.BaseRepository<DataModel.Sys_Log>, ISys_LogRepository
    {
        public Sys_LogRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}