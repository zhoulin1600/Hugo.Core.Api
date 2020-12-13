using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Hugo.Core.Common
{
    /// <summary>
    /// Json 拓展类
    /// </summary>
    public static class JsonExtension
    {
        static JsonExtension()
        {
            JsonConvert.DefaultSettings = () => DefaultJsonSetting;
        }
        public static JsonSerializerSettings DefaultJsonSetting = new JsonSerializerSettings
        {
            // 数据格式按原样输出 不使用驼峰样式的key
            ContractResolver = new DefaultContractResolver(),
            // 数据格式按原样输出 - 解析类型（驼峰大小写属性名称）的成员映射
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            // 获取或设置如何处理引用循环 - 忽略
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            // 获取或设置在序列化和反序列化期间如何处理空值 - 忽略
            NullValueHandling = NullValueHandling.Ignore,
            // 获取或设置处理日期格式 （ISO 8601 格式-默认 ，例如“2012-03-21T05:40Z”）（Microsoft JSON 格式，例如“\/Date（1198908717056）\/）”）
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            // 获取或设置日期格式字符串
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object ToObject(this string jsonStr, Type type)
        {
            return JsonConvert.DeserializeObject(jsonStr, type);
        }
    }
}
