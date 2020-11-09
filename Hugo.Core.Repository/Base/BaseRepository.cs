using Hugo.Core.Common;
using Hugo.Core.Common.ORM;
using Hugo.Core.IRepository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hugo.Core.Repository.Base
{
    /// <summary>
    /// 仓储层基类实现
    /// </summary>
    /// <typeparam name="TEntity">泛型数据实体</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarFactory _sqlSugarFactory;
        /// <summary>
        /// ISqlSugarClient
        /// </summary>
        protected ISqlSugarClient DB => this._sqlSugarFactory.GetDbContext();

        public BaseRepository(ISqlSugarFactory sqlSugarFactory) => _sqlSugarFactory = sqlSugarFactory;


        #region 新增

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert(TEntity entity)
        {
            return await DB.Insertable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <param name="entities">实体数据集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert(List<TEntity> entities)
        {
            return await DB.Insertable(entities).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entity">视图数据</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert<VEntity>(VEntity entity)
        {
            return await DB.Insertable(entity.ChangeType<TEntity>()).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entities">视图数据集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert<VEntity>(List<VEntity> entities)
        {
            return await DB.Insertable(entities.ChangeType<List<TEntity>>()).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 修改

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体数据（必须指定主键）</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await DB.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="strSql">UPDATE的SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(string strSql, object parameters = null)
        {
            return await DB.Ado.ExecuteCommandAsync(strSql, parameters) > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="updateColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(Expression<Func<TEntity, object>> updateColumnsExpression, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DB.Updateable<TEntity>().UpdateColumns(updateColumnsExpression).Where(whereExpression).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="setColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(Expression<Func<TEntity, TEntity>> setColumnsExpression, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DB.Updateable<TEntity>().SetColumns(setColumnsExpression).Where(whereExpression).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">更新数据</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="columnsExpression">更新列表达式</param>
        /// <param name="ignorecolumnsExpression">排除列表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(TEntity entity, Expression<Func<TEntity, bool>> whereExpression = null, Expression<Func<TEntity, object>> columnsExpression = null, Expression<Func<TEntity, object>> ignorecolumnsExpression = null)
        {
            IUpdateable<TEntity> up = await Task.Run(() => DB.Updateable(entity));
            if (whereExpression != null)
                up = await Task.Run(() => up.Where(whereExpression));
            if (columnsExpression != null)
                up = await Task.Run(() => up.UpdateColumns(columnsExpression));
            if (ignorecolumnsExpression != null)
                up = await Task.Run(() => up.IgnoreColumns(ignorecolumnsExpression));
            return await up.ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除指定主键Id的实体数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(object id)
        {
            return await DB.Deleteable<TEntity>(id).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除指定多个主键Id的实体数据
        /// </summary>
        /// <param name="ids">主键Id集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(object[] ids)
        {
            return await DB.Deleteable<TEntity>().In(ids).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除指定条件的实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DB.Deleteable<TEntity>().Where(whereExpression).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 存在判定
        /// </summary>
        /// <param name="db"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否存在</returns>
        public async Task<bool> Exist(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DB.Queryable<TEntity>().Where(whereExpression).AnyAsync();
        }

        /**************************************主键查询*********************************************/

        /// <summary>
        /// 查询指定主键Id的数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="cacheSeconds">缓存时长（单位：秒）</param>
        /// <returns>实体数据</returns>
        public async Task<TEntity> QueryById(object id, int cacheSeconds = 0)
        {
            return await DB.Queryable<TEntity>().WithCacheIF(cacheSeconds > 0, cacheSeconds).InSingleAsync(id);
        }

        /// <summary>
        /// 查询指定主键Id的数据
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="cacheSeconds">缓存时长（单位：秒）</param>
        /// <returns>视图数据</returns>
        public async Task<VEntity> QueryById<VEntity>(object id, int cacheSeconds = 0)
        {
            var model = await DB.Queryable<TEntity>().WithCacheIF(cacheSeconds > 0, cacheSeconds).InSingleAsync(id);
            return model.ChangeType<VEntity>();
        }

        /**************************************列表查询*********************************************/

        /// <summary>
        /// 查询所有实体数据
        /// </summary>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList()
        {
            return await DB.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DB.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(string whereString)
        {
            return await DB.Queryable<TEntity>().WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            return await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        {
            return await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        {
            return await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(string whereString, string orderFileds)
        {
            return await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(!string.IsNullOrEmpty(whereString), whereString).ToListAsync();
        }

        /*************************************列表TOP查询*******************************************/

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            return await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryTopList(int intTop, string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        {
            return await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        {
            return await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryTopList(int intTop, string whereString, string orderFileds)
        {
            return await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).Take(intTop).ToListAsync();
        }

        /************************************列表分页查询*******************************************/

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        public async Task<PageResult<TEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize)
        {
            PageResult<TEntity> result = new PageResult<TEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        public async Task<PageResult<TEntity>> QueryPageList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize)
        {
            PageResult<TEntity> result = new PageResult<TEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        public async Task<PageResult<TEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize)
        {
            PageResult<TEntity> result = new PageResult<TEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        public async Task<PageResult<TEntity>> QueryPageList(string whereString, string orderFileds, int pageIndex, int pageSize)
        {
            PageResult<TEntity> result = new PageResult<TEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /************************************列表分页查询（转视图模型）*******************************************/

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图数据对象</typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>视图数据列表</returns>
        public async Task<PageResult<VEntity>> QueryPageList<VEntity>(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize) where VEntity : class, new()
        {
            PageResult<VEntity> result = new PageResult<VEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(whereExpression != null, whereExpression).Select<VEntity>().ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图数据对象</typeparam>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>视图数据列表</returns>
        public async Task<PageResult<VEntity>> QueryPageList<VEntity>(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize) where VEntity : class, new()
        {
            PageResult<VEntity> result = new PageResult<VEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).Select<VEntity>().ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图数据对象</typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>视图数据列表</returns>
        public async Task<PageResult<VEntity>> QueryPageList<VEntity>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize) where VEntity : class, new()
        {
            PageResult<VEntity> result = new PageResult<VEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).Select<VEntity>().ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图数据对象</typeparam>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>视图数据列表</returns>
        public async Task<PageResult<VEntity>> QueryPageList<VEntity>(string whereString, string orderFileds, int pageIndex, int pageSize) where VEntity : class, new()
        {
            PageResult<VEntity> result = new PageResult<VEntity>();
            RefAsync<int> count = 0;
            result.PageData = await DB.Queryable<TEntity>().OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds).WhereIF(!string.IsNullOrWhiteSpace(whereString), whereString).Select<VEntity>().ToPageListAsync(pageIndex, pageSize, count);
            result.TotalCount = count;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.PageCount = (Math.Ceiling(result.TotalCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
            return result;
        }

        ///// <summary>
        ///// 查询分页实体数据
        ///// </summary>
        ///// <param name="queryCollection">查询集合</param>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页大小</param>
        ///// <returns>实体数据列表</returns>
        //public async Task<PageResult<VEntity>> QueryPageList<VEntity>(IQueryCollection queryCollection, Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize) where VEntity : class, new()
        //{
        //    List<IConditionalModel> conditionalModels = queryCollection.Cast<IConditionalModel>().ToList();
        //    PageResult<VEntity> result = new PageResult<VEntity>();
        //    RefAsync<int> count = 0;

        //    result.Data = await DB.Queryable<TEntity>()
        //        .OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds)
        //        .Where(conditionalModels)
        //        .WhereIF(whereExpression != null, whereExpression)
        //        .Select<VEntity>()
        //        .ToPageListAsync(pageIndex, pageSize, count);
        //    result.DataCount = count;
        //    result.PageIndex = pageIndex;
        //    result.PageSize = pageSize;
        //    result.PageCount = (Math.Ceiling(result.DataCount.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt();
        //    return result;
        //}

        #endregion

    }
}