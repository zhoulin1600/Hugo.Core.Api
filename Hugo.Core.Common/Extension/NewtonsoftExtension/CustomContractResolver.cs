using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hugo.Core.Common.NewtonsoftExtension
{
    public class CustomContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberSerialization">序列化成员</param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(x =>
                {
                    var property = CreateProperty(x, memberSerialization);

                    // Newtonsoft.Json 转换字符串 null 替换成string.Empty
                    property.ValueProvider = new StringNullToEmptyValueProvider(x);

                    return property;
                }).ToList();
        }

        /// <summary>
        /// 属性名称小写输出
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }

    }
}
