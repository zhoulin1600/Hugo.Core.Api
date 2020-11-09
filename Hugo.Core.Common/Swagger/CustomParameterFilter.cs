using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Swagger
{
    /// <summary>
    /// 自定义请求参数过滤器
    /// </summary>
    public class CustomParameterFilter : IOperationFilter
    {
        /// <summary>
        /// SwaggerAPI文档自定义请求参数过滤器应用
        /// </summary>
        /// <param name="operation">操作对象</param>
        /// <param name="context">操作过滤器上下文</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                //必需的。参数的名称。参数名区分大小写。
                Name = "access_token",
                // 必需的。参数的位置。可能的值是“query”、“header”、“路径”或“cookie”。
                In = ParameterLocation.Query,
                Style = ParameterStyle.Simple,
                //确定此参数是否为必需参数。如果参数位置是“path”，此属性是必需的，其值必须为true。否则属性可能包含在内，其默认值为false。
                Required = false,
                //参数的简要说明
                Description = "接口访问令牌"
            });

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    //必需的。参数的名称。参数名区分大小写。
            //    Name = "custompara",
            //    // 必需的。参数的位置。可能的值是“query”、“header”、“路径”或“cookie”。
            //    In = ParameterLocation.Header,
            //    Style = ParameterStyle.Simple,
            //    //确定此参数是否为必需参数。如果参数位置是“path”，此属性是必需的，其值必须为true。否则属性可能包含在内，其默认值为false。
            //    Required = false,
            //    //参数的简要说明
            //    Description = "自定义请求头参数筛选器"
            //});

        }

    }
}