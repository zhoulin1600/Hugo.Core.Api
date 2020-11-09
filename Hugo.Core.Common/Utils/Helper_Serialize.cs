using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 序列化帮助类
    /// <para>NuGet：Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson</para>
    /// </summary>
    public static class Helper_Serialize
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if (value == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }

        /// <summary>
        /// Json 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static string JsonSerialize(object value, params JsonConverter[] converters)
        {
            if (value != null)
            {
                if (converters != null && converters.Length > 0)
                {
                    return JsonConvert.SerializeObject(value, converters);
                }
                else
                {
                    if (value is DataSet)
                        return JsonConvert.SerializeObject(value, new DataSetConverter());
                    else if (value is DataTable)
                        return JsonConvert.SerializeObject(value, new DataTableConverter());
                    else
                        return JsonConvert.SerializeObject(value);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string value, params JsonConverter[] converters)
        {
            if (string.IsNullOrEmpty(value))
                return default(T);
            if (converters != null && converters.Length > 0)
            {
                return JsonConvert.DeserializeObject<T>(value, converters);
            }
            else
            {
                Type type = typeof(T);
                if (type == typeof(DataSet))
                {
                    return JsonConvert.DeserializeObject<T>(value, new DataSetConverter());
                }
                else if (type == typeof(DataTable))
                {
                    return JsonConvert.DeserializeObject<T>(value, new DataTableConverter());
                }
                return JsonConvert.DeserializeObject<T>(value);
            }
        }

    }
}
