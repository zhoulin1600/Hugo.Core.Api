using Hugo.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hugo.Core.IRepository.Base
{
    /// <summary>
    /// 仓储层基类接口
    /// </summary>
    /// <typeparam name="TEntity">泛型数据实体</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {

        #region 新增

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>是否成功</returns>
        Task<bool> Insert(TEntity entity);

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <param name="entities">实体数据集合</param>
        /// <returns>是否成功</returns>
        Task<bool> Insert(List<TEntity> entities);

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entity">视图数据</param>
        /// <returns>是否成功</returns>
        Task<bool> Insert<VEntity>(VEntity entity);

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entities">视图数据集合</param>
        /// <returns>是否成功</returns>
        Task<bool> Insert<VEntity>(List<VEntity> entities);

        #endregion

        #region 修改

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体数据（必须指定主键）</param>
        /// <returns>是否成功</returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="strSql">UPDATE的SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>是否成功</returns>
        Task<bool> Update(string strSql, object parameters = null);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="updateColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> Update(Expression<Func<TEntity, object>> updateColumnsExpression, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="setColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> Update(Expression<Func<TEntity, TEntity>> setColumnsExpression, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">更新数据</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="columnsExpression">更新列表达式</param>
        /// <param name="ignorecolumnsExpression">排除列表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> Update(TEntity entity, Expression<Func<TEntity, bool>> whereExpression = null, Expression<Func<TEntity, object>> columnsExpression = null, Expression<Func<TEntity, object>> ignorecolumnsExpression = null);

        #endregion

        #region 删除

        /// <summary>
        /// 删除指定主键Id的实体数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>是否成功</returns>
        Task<bool> Delete(object id);

        /// <summary>
        /// 删除指定多个主键Id的实体数据
        /// </summary>
        /// <param name="ids">主键Id集合</param>
        /// <returns>是否成功</returns>
        Task<bool> Delete(object[] ids);

        /// <summary>
        /// 删除指定条件的实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);

        #endregion

        #region 查询

        /// <summary>
        /// 存在判定
        /// </summary>
        /// <param name="db"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否存在</returns>
        Task<bool> Exist(Expression<Func<TEntity, bool>> whereExpression);

        /**************************************主键查询*********************************************/

        /// <summary>
        /// 查询指定主键Id的数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="cacheSeconds">缓存时长（单位：秒）</param>
        /// <returns>实体数据</returns>
        Task<TEntity> QueryById(object id, int cacheSeconds = 0);

        /// <summary>
        /// 查询指定主键Id的数据
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="cacheSeconds">缓存时长（单位：秒）</param>
        /// <returns>视图数据</returns>
        Task<VEntity> QueryById<VEntity>(object id, int cacheSeconds = 0);

        /**************************************列表查询*********************************************/

        /// <summary>
        /// 查询所有实体数据
        /// </summary>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList();

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList(string whereString);

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds);

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true);

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true);

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryList(string whereString, string orderFileds);

        /*************************************列表TOP查询*******************************************/

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, string orderFileds);

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryTopList(int intTop, string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true);

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true);

        /// <summary>
        /// 查询Top实体数据
        /// </summary>
        /// <param name="intTop">Top数量</param>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        Task<List<TEntity>> QueryTopList(int intTop, string whereString, string orderFileds);

        /************************************列表分页查询*******************************************/

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        Task<PageResult<TEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize);

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        Task<PageResult<TEntity>> QueryPageList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize);

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        Task<PageResult<TEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize);

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>实体数据列表</returns>
        Task<PageResult<TEntity>> QueryPageList(string whereString, string orderFileds, int pageIndex, int pageSize);

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
        Task<PageResult<VEntity>> QueryPageList<VEntity>(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize) where VEntity : class, new();

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
        Task<PageResult<VEntity>> QueryPageList<VEntity>(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize) where VEntity : class, new();

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
        Task<PageResult<VEntity>> QueryPageList<VEntity>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize) where VEntity : class, new();

        /// <summary>
        /// 查询分页实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图数据对象</typeparam>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>视图数据列表</returns>
        Task<PageResult<VEntity>> QueryPageList<VEntity>(string whereString, string orderFileds, int pageIndex, int pageSize) where VEntity : class, new();

        #endregion

    }
}

