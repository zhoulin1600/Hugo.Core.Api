using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.ORM
{
    public static class SqlSugarFactoryExtension
    {
        #region 根据主键获取实体对象

        ///// <summary>
        ///// 根据主键获取实体对象
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static TSource GetById<TSource>(this SqlSugarClient db, dynamic id) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().InSingle(id);
        //}

        ///// <summary>
        ///// 根据主键获取实体对象
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static TMap GetById<TSource, TMap>(this SqlSugarClient db, dynamic id) where TSource : EntityBase, new()
        //{
        //    TSource model = db.Queryable<TSource>().InSingle(id);
        //    return model.Map<TSource, TMap>();
        //}

        #endregion

        #region 根据Linq表达式条件获取单个实体对象

        ///// <summary>
        ///// 根据条件获取单个实体对象
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp"></param>
        ///// <returns></returns>
        //public static TSource Get<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().Where(whereExp).Single();
        //}

        ///// <summary>
        ///// 根据条件获取单个实体对象
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static TMap Get<TSource, TMap>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    TSource model = db.Queryable<TSource>().Where(whereExp).Single();
        //    return model.Map<TSource, TMap>();
        //}

        #endregion

        #region 获取所有实体列表

        ///// <summary>
        ///// 获取所有实体列表
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <returns></returns>
        //public static List<TSource> GetList<TSource>(this SqlSugarClient db) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().ToList();
        //}

        ///// <summary>
        ///// 获取实体列表
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <returns></returns>
        //public static List<TMap> GetList<TSource, TMap>(this SqlSugarClient db) where TSource : EntityBase, new()
        //{
        //    var result = db.Queryable<TSource>().ToList();
        //    return result.Map<List<TSource>, List<TMap>>();
        //}

        #endregion

        #region 根据Linq表达式条件获取列表

        ///// <summary>
        ///// 根据条件获取实体列表
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static List<TSource> GetList<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().Where(whereExp).ToList();
        //}

        ///// <summary>
        ///// 根据条件获取实体列表
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static List<TMap> GetList<TSource, TMap>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    var result = db.Queryable<TSource>().Where(whereExp).ToList();
        //    return result.Map<List<TSource>, List<TMap>>();
        //}

        #endregion

        #region 根据Sugar条件获取列表

        ///// <summary>
        ///// 根据条件获取实体列表
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar调价表达式集合</param>
        ///// <returns></returns>
        //public static List<TSource> GetList<TSource>(this SqlSugarClient db, List<IConditionalModel> conditionals) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().Where(conditionals).ToList();
        //}

        ///// <summary>
        ///// 根据条件获取实体列表
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar调价表达式集合</param>
        ///// <returns></returns>
        //public static List<TMap> GetList<TSource, TMap>(this SqlSugarClient db, List<IConditionalModel> conditionals) where TSource : EntityBase, new()
        //{
        //    var result = db.Queryable<TSource>().Where(conditionals).ToList();
        //    return result.Map<List<TSource>, List<TMap>>();
        //}

        #endregion

        #region 是否包含某个元素
        ///// <summary>
        ///// 是否包含某个元素
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static bool Exist<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    return db.Queryable<TSource>().Where(whereExp).Any();
        //}
        #endregion

        #region 新增实体对象
        ///// <summary>
        ///// 新增实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="insertObj"></param>
        ///// <returns></returns>
        //public static bool Insert<TSource>(this SqlSugarClient db, TSource insertObj) where TSource : EntityBase, new()
        //{
        //    return db.Insertable(insertObj).ExecuteCommand() > 0;
        //}

        ///// <summary>
        ///// 新增实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <typeparam name="TMap"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="insertDto"></param>
        ///// <returns></returns>
        //public static bool Insert<TSource, TMap>(this SqlSugarClient db, TSource insertDto) where TMap : EntityBase, new()
        //{
        //    var entity = insertDto.Map<TSource, TMap>();
        //    return db.Insertable(entity).ExecuteCommand() > 0;
        //}
        #endregion

        #region 批量新增实体对象
        ///// <summary>
        ///// 批量新增实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="insertObjs"></param>
        ///// <returns></returns>
        //public static bool InsertRange<TSource>(this SqlSugarClient db, List<TSource> insertObjs) where TSource : EntityBase, new()
        //{
        //    return db.Insertable(insertObjs).ExecuteCommand() > 0;
        //}

        ///// <summary>
        ///// 批量新增实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <typeparam name="TMap"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="insertObjs"></param>
        ///// <returns></returns>
        //public static bool InsertRange<TSource, TMap>(this SqlSugarClient db, List<TSource> insertObjs) where TMap : EntityBase, new()
        //{
        //    var entitys = insertObjs.Map<List<TSource>, List<TMap>>();
        //    return db.Insertable(entitys).ExecuteCommand() > 0;
        //}
        #endregion

        #region 更新单个实体对象
        ///// <summary>
        ///// 更新单个实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="updateObj"></param>
        ///// <returns></returns>
        //public static bool Update<TSource>(this SqlSugarClient db, TSource updateObj) where TSource : EntityBase, new()
        //{
        //    return db.Updateable(updateObj).ExecuteCommand() > 0;
        //}
        #endregion

        #region 根据条件批量更新实体指定列
        ///// <summary>
        ///// 根据条件批量更新实体指定列
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="columns">需要更新的列</param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static bool Update<TSource>(this SqlSugarClient db, Expression<Func<TSource, TSource>> columns, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    return db.Updateable<TSource>().UpdateColumns(columns).Where(whereExp).ExecuteCommand() > 0;
        //}
        #endregion

        #region 物理删除实体对象

        ///// <summary>
        ///// 物理删除实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="deleteObj"></param>
        ///// <returns></returns>
        //public static bool Delete<TSource>(this SqlSugarClient db, TSource deleteObj) where TSource : EntityBase, new()
        //{
        //    return db.Deleteable<TSource>().Where(deleteObj).ExecuteCommand() > 0;
        //}

        ///// <summary>
        ///// 物理删除实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">条件表达式</param>
        ///// <returns></returns>
        //public static bool Delete<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    return db.Deleteable<TSource>().Where(whereExp).ExecuteCommand() > 0;
        //}

        ///// <summary>
        ///// 根据主键物理删除实体对象
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static bool DeleteById<TSource>(this SqlSugarClient db, dynamic id) where TSource : EntityBase, new()
        //{
        //    return db.Deleteable<TSource>().In(id).ExecuteCommand() > 0;
        //}

        ///// <summary>
        ///// 根据主键批量物理删除实体集合
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public static bool DeleteByIds<TSource>(this SqlSugarClient db, dynamic[] ids) where TSource : EntityBase, new()
        //{
        //    return db.Deleteable<TSource>().In(ids).ExecuteCommand() > 0;
        //}

        #endregion

        //#region 分页查询

        ///// <summary>
        ///// 获取分页列表【页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        ///// 获取分页列表【页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询（排序）

        ///// <summary>
        ///// 获取分页列表【排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        ///// 获取分页列表【排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询（Linq表达式条件）

        ///// <summary>
        ///// 获取分页列表【Linq表达式条件，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">Linq表达式条件</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(whereExp).ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        ///// 获取分页列表【Linq表达式条件，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">Linq表达式条件</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(whereExp).ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询（Linq表达式条件，排序）

        ///// <summary>
        ///// 获取分页列表【Linq表达式条件，排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">Linq表达式条件</param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(whereExp).OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        /////  获取分页列表【Linq表达式条件，排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="whereExp">Linq表达式条件</param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, Expression<Func<TSource, bool>> whereExp, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(whereExp).OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询（Sugar条件）

        ///// <summary>
        ///// 获取分页列表【Sugar表达式条件，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar条件表达式集合</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, List<IConditionalModel> conditionals, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(conditionals).ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        ///// 获取分页列表【Sugar表达式条件，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource">数据源类型</typeparam>
        ///// <typeparam name="TMap">数据源映射类型</typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar条件表达式集合</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, List<IConditionalModel> conditionals, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(conditionals).ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询（Sugar条件，排序）

        ///// <summary>
        /////  获取分页列表【Sugar表达式条件，排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar条件表达式集合</param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TSource> GetPageList<TSource>(this SqlSugarClient db, List<IConditionalModel> conditionals, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(conditionals).OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    return new PagedList<TSource>(result, pageIndex, pageSize, count);
        //}

        ///// <summary>
        /////  获取分页列表【Sugar表达式条件，排序，页码，每页条数】
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="conditionals">Sugar条件表达式集合</param>
        ///// <param name="orderExp">排序表达式</param>
        ///// <param name="orderType">排序类型</param>
        ///// <param name="pageIndex">页码（从0开始）</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, List<IConditionalModel> conditionals, Expression<Func<TSource, object>> orderExp, OrderByType orderType, int pageIndex, int pageSize) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    var result = db.Queryable<TSource>().Where(conditionals).OrderBy(orderExp, orderType).ToPageList(pageIndex, pageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, pageIndex, pageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}

        //#endregion

        //#region 分页查询 （扩展条件构造实体，默认排序列，默认排序方式）
        ///// <summary>
        ///// 分页查询 （扩展条件构造实体，默认排序列，默认排序方式）
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <typeparam name="TMap"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="query"></param>
        ///// <param name="defaultSort"></param>
        ///// <param name="defaultSortType"></param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, QueryCollection query, Expression<Func<TSource, object>> defaultSort, OrderByType defaultSortType) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    List<IConditionalModel> conditionals = query.ConditionItems.ExamineConditional<TSource>();
        //    Expression<Func<TSource, object>> sort = query.SortLambda<TSource, object>(defaultSort, defaultSortType, out var sortType);
        //    var result = db.Queryable<TSource>().Where(conditionals).OrderBy(sort, sortType).ToPageList(query.PageIndex, query.PageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, query.PageIndex, query.PageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}
        //#endregion

        //#region 分页查询 （扩展条件构造实体，默认排序列，默认排序方式，Linq表达式条件）
        ///// <summary>
        ///// 分页查询 （扩展条件构造实体，默认排序列，默认排序方式，Linq表达式条件）
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <typeparam name="TMap"></typeparam>
        ///// <param name="db"></param>
        ///// <param name="query"></param>
        ///// <param name="defaultSort"></param>
        ///// <param name="defaultSortType"></param>
        ///// <param name="whereExp"></param>
        ///// <returns></returns>
        //public static IPagedList<TMap> GetPageList<TSource, TMap>(this SqlSugarClient db, QueryCollection query, Expression<Func<TSource, object>> defaultSort, OrderByType defaultSortType, Expression<Func<TSource, bool>> whereExp) where TSource : EntityBase, new()
        //{
        //    int count = 0;
        //    List<IConditionalModel> conditionals = query.ConditionItems.ExamineConditional<TSource>();
        //    Expression<Func<TSource, object>> sort = query.SortLambda<TSource, object>(defaultSort, defaultSortType, out var sortType);
        //    var result = db.Queryable<TSource>().Where(whereExp).Where(conditionals).OrderBy(sort, sortType).ToPageList(query.PageIndex, query.PageSize, ref count);
        //    var pageResult = new PagedList<TSource>(result, query.PageIndex, query.PageSize, count);
        //    return pageResult.Map<TSource, TMap>();
        //}
        //#endregion
    }
}
