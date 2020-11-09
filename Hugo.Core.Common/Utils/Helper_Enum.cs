﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class Helper_Enum
    {
        /// <summary>
        /// 将枚举类型转为选项列表
        /// 注：value为值,text为显示内容
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<object> ToOptionList(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            List<object> list = new List<object>();
            foreach (var aValue in values)
            {
                list.Add(new
                {
                    value = ((int)aValue).ToString(),
                    text = aValue.ToString()
                });
            }

            return list;
        }

        /// <summary>
        /// 将枚举类型转为选项列表
        /// 注：value为值,text为显示内容
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<object> ToDescriptionList(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            List<object> list = new List<object>();
            foreach (var item in values)
                list.Add(new { value = (int)item, text = ((Enum)item).GetDescription() });
            return list;
        }

        /// <summary>
        /// 多选枚举转为对应文本,逗号隔开
        /// </summary>
        /// <param name="values">多个值</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string ToMultipleText(List<int> values, Type enumType)
        {
            if (values == null)
                return string.Empty;

            List<string> textList = new List<string>();

            var allValues = Enum.GetValues(enumType);
            foreach (var aValue in allValues)
            {
                if (values.Contains((int)aValue))
                    textList.Add(aValue.ToString());
            }

            return string.Join(",", textList);
        }

        /// <summary>
        /// 多选枚举转为对应文本,逗号隔开
        /// </summary>
        /// <param name="values">多个值逗号隔开</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string ToMultipleText(string values, Type enumType)
        {
            return ToMultipleText(values?.Split(',')?.Select(x => x.ToInt())?.ToList(), enumType);
        }
    }
}