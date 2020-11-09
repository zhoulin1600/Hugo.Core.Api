﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 全局数据
    /// </summary>
    public static class GlobalData
    {
        static GlobalData()
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AllFxAssemblies = Directory.GetFiles(rootPath, "*.dll")
                .Where(x => new FileInfo(x).Name.Contains(FXASSEMBLY_PATTERN))
                .Select(x => Assembly.LoadFrom(x))
                .Where(x => !x.IsDynamic)
                .ToList();

            AllFxAssemblies.ForEach(aAssembly =>
            {
                try
                {
                    AllFxTypes.AddRange(aAssembly.GetTypes());
                }
                catch
                {

                }
            });
        }

        /// <summary>
        /// 解决方案程序集匹配名
        /// </summary>
        public const string FXASSEMBLY_PATTERN = "Hugo";

        /// <summary>
        /// 解决方案所有程序集
        /// </summary>
        public static readonly List<Assembly> AllFxAssemblies;

        /// <summary>
        /// 解决方案所有自定义类
        /// </summary>
        public static readonly List<Type> AllFxTypes = new List<Type>();

        /// <summary>
        /// 超级管理员UserIId
        /// </summary>
        public const string ADMINID = "Admin";

        #region 自定义系统角色
        /// <summary>
        /// 超级管理员 AUTH_ADMINISTRATOR
        /// </summary>
        public const string AUTH_ADMIN = "超级管理员";

        /// <summary>
        /// 管理员 MANAGER
        /// </summary>
        public const string AUTH_MANAGER = "管理端";

        /// <summary>
        /// 客户端 CLIENT
        /// </summary>
        public const string AUTH_CLIENT = "客户端";
        #endregion

        /// <summary>
        /// 权限策略名称
        /// </summary>
        public const string Authority_Policy_Name = "Authority";

    }
}
