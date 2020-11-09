using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统功能信息
    /// </summary>
    public class Sys_ActionRepository : Base.BaseRepository<DataModel.Sys_Action>, ISys_ActionRepository
    {
        public Sys_ActionRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}