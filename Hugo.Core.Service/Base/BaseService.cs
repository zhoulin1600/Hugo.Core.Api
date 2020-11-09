using Hugo.Core.Common;
using Hugo.Core.IRepository.Base;
using Hugo.Core.IService.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hugo.Core.Service.Base
{
    /// <summary>
    /// 服务层基类实现
    /// </summary>
    /// <typeparam name="VEntity">泛型数据视图</typeparam>
    /// <typeparam name="TEntity">泛型数据实体</typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IBaseRepository<TEntity> BaseRepository;//通过在子类的构造函数中注入，基类无需构造
        //protected readonly ISqlSugarFactory _sqlSugarFactory;
        //protected readonly IMapper _mapper;
        //protected readonly ILogger _logger;

        ///// <summary>
        ///// SqlSugarClient
        ///// </summary>
        //protected ISqlSugarClient DB => this._sqlSugarFactory.GetDbContext();

        //public BaseService() { }

        public BaseService(IBaseRepository<TEntity> baseRepository) => BaseRepository = baseRepository;

        //public BaseService(ISqlSugarFactory sqlSugarFactory) => _sqlSugarFactory = sqlSugarFactory;

        //public BaseService(ISqlSugarFactory sqlSugarFactory, IBaseRepository<TEntity> baseRepository) : this(sqlSugarFactory) => _baseRepository = baseRepository;

        //public BaseService(ISqlSugarFactory sqlSugarFactory, IBaseRepository<TEntity> baseRepository, IMapper mapper) : this(sqlSugarFactory, baseRepository) => _mapper = mapper;

        //public BaseService(ISqlSugarFactory sqlSugarFactory, IBaseRepository<TEntity> baseRepository, IMapper mapper, ILogger<BaseService<VEntity, TEntity>> logger) : this(sqlSugarFactory, baseRepository, mapper) => _logger = logger;

        //public BaseService(ISqlSugarFactory sqlSugarFactory, IBaseRepository<TEntity> baseRepository) : this(sqlSugarFactory) => _baseRepository = baseRepository;


        #region 新增

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert(TEntity entity)
        {
            return await BaseRepository.Insert(entity);
        }

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <param name="entities">实体数据集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert(List<TEntity> entities)
        {
            return await BaseRepository.Insert(entities);
        }

        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entity">视图数据</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert<VEntity>(VEntity entity)
        {
            return await BaseRepository.Insert<VEntity>(entity);
        }

        /// <summary>
        /// 新增实体数据集合
        /// </summary>
        /// <typeparam name="VEntity">视图对象</typeparam>
        /// <param name="entities">视图数据集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Insert<VEntity>(List<VEntity> entities)
        {
            return await BaseRepository.Insert<VEntity>(entities);
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
            return await BaseRepository.Update(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="strSql">UPDATE的SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(string strSql, object parameters = null)
        {
            return await BaseRepository.Update(strSql, parameters);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="updateColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(Expression<Func<TEntity, object>> updateColumnsExpression, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseRepository.Update(updateColumnsExpression, whereExpression);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="setColumnsExpression">更新列表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Update(Expression<Func<TEntity, TEntity>> setColumnsExpression, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseRepository.Update(setColumnsExpression, whereExpression);
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
            return await BaseRepository.Update(entity, whereExpression, columnsExpression, ignorecolumnsExpression);
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
            return await BaseRepository.Delete(id);
        }

        /// <summary>
        /// 删除指定多个主键Id的实体数据
        /// </summary>
        /// <param name="ids">主键Id集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(object[] ids)
        {
            return await BaseRepository.Delete(ids);
        }

        /// <summary>
        /// 删除指定条件的实体数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseRepository.Delete(whereExpression);
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
            return await BaseRepository.Exist(whereExpression);
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
            return await BaseRepository.QueryById(id, cacheSeconds);
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
            return await BaseRepository.QueryById<VEntity>(id, cacheSeconds);
        }

        /**************************************列表查询*********************************************/

        /// <summary>
        /// 查询所有实体数据
        /// </summary>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList()
        {
            return await BaseRepository.QueryList();
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseRepository.QueryList(whereExpression);
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(string whereString)
        {
            return await BaseRepository.QueryList(whereString);
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            return await BaseRepository.QueryList(whereExpression, orderFileds);
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
            return await BaseRepository.QueryList(whereString, orderExpression, isAsc);
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
            return await BaseRepository.QueryList(whereExpression, orderExpression, isAsc);
        }

        /// <summary>
        /// 查询实体数据列表
        /// </summary>
        /// <param name="whereString">SQL条件</param>
        /// <param name="orderFileds">排序字段（name asc, age desc）</param>
        /// <returns>实体数据列表</returns>
        public async Task<List<TEntity>> QueryList(string whereString, string orderFileds)
        {
            return await BaseRepository.QueryList(whereString, orderFileds);
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
            return await BaseRepository.QueryTopList(intTop, whereExpression, orderFileds);
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
            return await BaseRepository.QueryTopList(intTop, whereString, orderExpression, isAsc);
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
            return await BaseRepository.QueryTopList(intTop, whereExpression, orderExpression, isAsc);
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
            return await BaseRepository.QueryTopList(intTop, whereString, orderFileds);
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
            return await BaseRepository.QueryPageList(whereExpression, orderFileds, pageIndex, pageSize);
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
            return await BaseRepository.QueryPageList(whereString, orderExpression, isAsc, pageIndex, pageSize);
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
            return await BaseRepository.QueryPageList(whereExpression, orderExpression, isAsc, pageIndex, pageSize);
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
            return await BaseRepository.QueryPageList(whereString, orderFileds, pageIndex, pageSize);
        }

        #endregion



        ///**************************************↓弃用↓*********************************************/

        //#region 新增

        ///// <summary>
        ///// 新增视图数据
        ///// </summary>
        ///// <param name="entity">视图数据</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Insert(VEntity entity)
        //{
        //    return await _baseRepository.Insert(_mapper.Map<TEntity>(entity));
        //}

        ///// <summary>
        ///// 新增多个视图数据
        ///// </summary>
        ///// <param name="entities">视图数据集合</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Insert(List<VEntity> entities)
        //{
        //    return await _baseRepository.Insert(_mapper.Map<List<TEntity>>(entities));
        //}

        //#endregion

        //#region 修改

        ///// <summary>
        ///// 更新视图数据
        ///// </summary>
        ///// <param name="entity">视图数据（必须指定主键）</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Update(VEntity entity)
        //{
        //    return await _baseRepository.Update(_mapper.Map<TEntity>(entity));
        //}

        ///// <summary>
        ///// 更新视图数据
        ///// </summary>
        ///// <param name="strSql">UPDATE的SQL语句</param>
        ///// <param name="parameters">参数集合</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Update(string strSql, object parameters = null)
        //{
        //    return await _baseRepository.Update(strSql, parameters);
        //}

        ///// <summary>
        ///// 更新视图数据
        ///// </summary>
        ///// <param name="entity">更新数据</param>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="columnsExpression">更新列表达式</param>
        ///// <param name="ignorecolumnsExpression">排除列表达式</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Update(VEntity entity, Expression<Func<TEntity, bool>> whereExpression = null, Expression<Func<TEntity, object>> columnsExpression = null, Expression<Func<TEntity, object>> ignorecolumnsExpression = null)
        //{
        //    return await _baseRepository.Update(_mapper.Map<TEntity>(entity), whereExpression, columnsExpression, ignorecolumnsExpression);
        //}

        //#endregion

        //#region 删除

        ///// <summary>
        ///// 删除指定主键Id的视图数据
        ///// </summary>
        ///// <param name="id">主键Id</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Delete(object id)
        //{
        //    return await _baseRepository.Delete(id);
        //}

        ///// <summary>
        ///// 删除指定多个主键Id的视图数据
        ///// </summary>
        ///// <param name="ids">主键Id集合</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Delete(object[] ids)
        //{
        //    return await _baseRepository.Delete(ids);
        //}

        ///// <summary>
        ///// 删除指定条件的视图数据
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        //{
        //    return await _baseRepository.Delete(whereExpression);
        //}

        //#endregion

        //#region 查询

        ///**************************************主键查询*********************************************/

        ///// <summary>
        ///// 查询指定主键Id的数据
        ///// </summary>
        ///// <param name="id">主键Id</param>
        ///// <param name="iscache">是否使用缓存</param>
        ///// <returns>视图数据</returns>
        //public async Task<VEntity> QueryById(object id, bool iscache = false)
        //{
        //    var entity = await _baseRepository.QueryById(id, iscache);
        //    return _mapper.Map<VEntity>(entity);
        //}

        ///// <summary>
        ///// 查询指定多个主键Id的数据
        ///// </summary>
        ///// <param name="ids">主键Id集合</param>
        ///// <param name="iscache">是否使用缓存</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryById(object[] ids, bool iscache = false)
        //{
        //    var entities = await _baseRepository.QueryById(ids, iscache);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///**************************************列表查询*********************************************/

        ///// <summary>
        ///// 查询所有视图数据
        ///// </summary>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList()
        //{
        //    var entities = await _baseRepository.QueryList();
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <returns>数据列表</returns>
        //public async Task<List<VEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression)
        //{
        //    var entities = await _baseRepository.QueryList(whereExpression);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereString">SQL条件</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList(string whereString)
        //{
        //    var entities = await _baseRepository.QueryList(whereString);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        //{
        //    var entities = await _baseRepository.QueryList(whereExpression, orderFileds);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        //{
        //    var entities = await _baseRepository.QueryList(whereString, orderExpression, isAsc);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        //{
        //    var entities = await _baseRepository.QueryList(whereExpression, orderExpression, isAsc);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询视图数据列表
        ///// </summary>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryList(string whereString, string orderFileds)
        //{
        //    var entities = await _baseRepository.QueryList(whereString, orderFileds);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///*************************************列表TOP查询*******************************************/

        ///// <summary>
        ///// 查询Top视图数据
        ///// </summary>
        ///// <param name="intTop">Top数量</param>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        //{
        //    var entities = await _baseRepository.QueryTopList(intTop, whereExpression, orderFileds);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询Top视图数据
        ///// </summary>
        ///// <param name="intTop">Top数量</param>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryTopList(int intTop, string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        //{
        //    var entities = await _baseRepository.QueryTopList(intTop, whereString, orderExpression);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询Top视图数据
        ///// </summary>
        ///// <param name="intTop">Top数量</param>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryTopList(int intTop, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        //{
        //    var entities = await _baseRepository.QueryTopList(intTop, whereExpression, orderExpression , isAsc);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///// <summary>
        ///// 查询Top视图数据
        ///// </summary>
        ///// <param name="intTop">Top数量</param>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<List<VEntity>> QueryTopList(int intTop, string whereString, string orderFileds)
        //{
        //    var entities = await _baseRepository.QueryTopList(intTop, whereString, orderFileds);
        //    return _mapper.Map<List<VEntity>>(entities);
        //}

        ///************************************列表分页查询*******************************************/

        ///// <summary>
        ///// 查询分页视图数据
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页大小</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<PageResult<VEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize)
        //{
        //    var pageResult = await _baseRepository.QueryPageList(whereExpression, orderFileds, pageIndex, pageSize);
        //    var result = new PageResult<VEntity>();
        //    result.PageIndex = pageResult.PageIndex;
        //    result.PageSize = pageResult.PageSize;
        //    result.PageCount = pageResult.PageCount;
        //    result.DataCount = pageResult.DataCount;
        //    result.Data = _mapper.Map<List<VEntity>>(pageResult.Data);
        //    return result;
        //}

        ///// <summary>
        ///// 查询分页视图数据
        ///// </summary>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页大小</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<PageResult<VEntity>> QueryPageList(string whereString, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize)
        //{
        //    var pageResult = await _baseRepository.QueryPageList(whereString, orderExpression, isAsc, pageIndex, pageSize);
        //    var result = new PageResult<VEntity>();
        //    result.PageIndex = pageResult.PageIndex;
        //    result.PageSize = pageResult.PageSize;
        //    result.PageCount = pageResult.PageCount;
        //    result.DataCount = pageResult.DataCount;
        //    result.Data = _mapper.Map<List<VEntity>>(pageResult.Data);
        //    return result;
        //}

        ///// <summary>
        ///// 查询分页视图数据
        ///// </summary>
        ///// <param name="whereExpression">条件表达式</param>
        ///// <param name="orderExpression">排序表达式</param>
        ///// <param name="isAsc">是否升序</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页大小</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<PageResult<VEntity>> QueryPageList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc, int pageIndex, int pageSize)
        //{
        //    var pageResult = await _baseRepository.QueryPageList(whereExpression, orderExpression, isAsc, pageIndex, pageSize);
        //    var result = new PageResult<VEntity>();
        //    result.PageIndex = pageResult.PageIndex;
        //    result.PageSize = pageResult.PageSize;
        //    result.PageCount = pageResult.PageCount;
        //    result.DataCount = pageResult.DataCount;
        //    result.Data = _mapper.Map<List<VEntity>>(pageResult.Data);
        //    return result;
        //}

        ///// <summary>
        ///// 查询分页视图数据
        ///// </summary>
        ///// <param name="whereString">SQL条件</param>
        ///// <param name="orderFileds">排序字段（name asc, age desc）</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页大小</param>
        ///// <returns>视图数据列表</returns>
        //public async Task<PageResult<VEntity>> QueryPageList(string whereString, string orderFileds, int pageIndex, int pageSize)
        //{
        //    var pageResult = await _baseRepository.QueryPageList(whereString, orderFileds, pageIndex, pageSize);
        //    var result = new PageResult<VEntity>();
        //    result.PageIndex = pageResult.PageIndex;
        //    result.PageSize = pageResult.PageSize;
        //    result.PageCount = pageResult.PageCount;
        //    result.DataCount = pageResult.DataCount;
        //    result.Data = _mapper.Map<List<VEntity>>(pageResult.Data);
        //    return result;
        //}

        //#endregion

    }
}