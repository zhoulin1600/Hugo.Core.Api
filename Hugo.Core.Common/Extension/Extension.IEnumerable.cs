using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hugo.Core.Common
{
    /// <summary>
    /// IEnumerable 拓展类
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// IEnumerable拓展 - Copy 序列数据复制
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="startIndex">原数据开始复制的起始位置</param>
        /// <param name="length">需要复制的数据长度</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Copy<TSource>(this IEnumerable<TSource> source, int startIndex, int length)
        {
            var sourceArray = source.ToArray();
            TSource[] newArray = new TSource[length];
            Array.Copy(sourceArray, startIndex, newArray, 0, length);

            return newArray;
        }

        /// <summary>
        /// IEnumerable拓展 - Distinct 序列数据去重（使用方式：list = list.DistinctList(c => new { c.Id }).ToList();）
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <typeparam name="TKey">去重类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keySelector">去重字段</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// IEnumerable拓展 - ForEach
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="func">操作方法</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> func)
        {
            foreach (var item in source)
            {
                func(item);
            }
        }

        /// <summary>
        /// IEnumerable拓展 - ForEach
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="func">操作方法</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> func)
        {
            var array = source.ToArray();
            for (int i = 0; i < array.Count(); i++)
            {
                func(array[i], i);
            }
        }

        /// <summary>
        /// IEnumerable 转 List
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static List<TSource> CastToList<TSource>(this IEnumerable source)
        {
            return new List<TSource>(source.Cast<TSource>());
        }

        /// <summary>
        /// IEnumerable 转 DataTable
        /// </summary>
        /// <typeparam name="TSource">数据模型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>DataTable</returns>
        public static DataTable CastToDataTable<TSource>(this IEnumerable<TSource> source)
        {
            return source.ToJson().ToDataTable();
        }

        ///// <summary>
        ///// 获取分页数据(包括总数量)
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="iEnumberable">数据源</param>
        ///// <param name="pageInput">分页参数</param>
        ///// <returns></returns>
        //public static PageResult<T> GetPageResult<T>(this IEnumerable<T> iEnumberable, PageInput pageInput)
        //{
        //    int count = iEnumberable.Count();

        //    var list = iEnumberable.AsQueryable()
        //        .OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
        //        .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
        //        .Take(pageInput.PageRows)
        //        .ToList();

        //    return new PageResult<T> { Data = list, Total = count };
        //}

        ///// <summary>
        ///// 获取分页数据(仅获取列表,不获取总数量)
        ///// </summary>
        ///// <typeparam name="T">泛型</typeparam>
        ///// <param name="iEnumberable">数据源</param>
        ///// <param name="pageInput">分页参数</param>
        ///// <returns></returns>
        //public static List<T> GetPageList<T>(this IEnumerable<T> iEnumberable, PageInput pageInput)
        //{
        //    var list = iEnumberable.AsQueryable()
        //        .OrderBy($@"{pageInput.SortField} {pageInput.SortType}")
        //        .Skip((pageInput.PageIndex - 1) * pageInput.PageRows)
        //        .Take(pageInput.PageRows)
        //        .ToList();

        //    return list;
        //}
    }
}