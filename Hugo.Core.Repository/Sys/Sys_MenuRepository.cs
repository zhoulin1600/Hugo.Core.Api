using Hugo.Core.Common.ORM;
using Hugo.Core.DataModel;
using Hugo.Core.IRepository;
using Hugo.Core.Repository.Base;

namespace Hugo.Core.Repository
{
    /// <summary>
    /// 仓储实现：系统菜单信息
    /// </summary>
    public class Sys_MenuRepository : Base.BaseRepository<DataModel.Sys_Menu>, ISys_MenuRepository
    {
        public Sys_MenuRepository(ISqlSugarFactory sqlSugarFactory) : base(sqlSugarFactory) { }
        
    }
}