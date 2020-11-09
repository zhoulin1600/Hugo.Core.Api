using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.Swagger
{
    /// <summary>
    /// 自定义Api接口版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本
        /// </summary>
        public enum ApiVersion
        {
            /// <summary>
            /// v1 版本
            /// </summary>
            v1 = 1,

            /// <summary>
            /// v2 版本
            /// </summary>
            v2 = 2,
        }
    }
}
