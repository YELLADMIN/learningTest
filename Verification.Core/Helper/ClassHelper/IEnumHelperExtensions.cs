﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Verification.Core.Helper
{
    public static class IEnumHelperExtensions
    {
        #region 获取结构

        /// <summary>
        /// 获取枚举的结构
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举对应的值类型</typeparam>
        /// <param name="enumHelper"></param>
        /// <param name="convert">指时如何从<see cref="object"/>转成<typeparamref name="TValue"/></param>
        /// <returns></returns>
        [return: NotNull]
        public static IReadOnlyDictionary<string, TValue> GetEnumStruct<TEnum, TValue>(
            this IEnumHelper enumHelper,
            [NotNull] Func<EnumAttributeData, TValue> convert)
            where TEnum : Enum
        {
            convert.CheckNull(nameof(convert));
            Dictionary<string, TValue> resultList = new Dictionary<string, TValue>();

            //遍历数据并组合成Dictionary
            foreach (var item in enumHelper.GetAllEnumAttributeData<TEnum>())//获取枚举结构数据
            {
                try
                {
                    TValue value = convert(item);
                    resultList.Add(item.EnumValueName, value);
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidCastException(
                        $"获取枚举结构时，枚举值的类型传递错误。枚举名:{item.EnumType.Name}",
                        ex);
                }
            }

            return resultList;
        }

        /// <summary>
        /// 获取枚举的结构
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <paramref name="enumHelper"></paramref>
        /// <returns></returns>
        [return: NotNull]
        public static IReadOnlyDictionary<string, int> GetEnumStruct<TEnum>(
            this IEnumHelper enumHelper)
            where TEnum : Enum
        {
            return IEnumHelperExtensions.GetEnumStruct<TEnum, int>(enumHelper, t => (int)t.BaseValue);
        }

        /// <summary>
        /// 获取枚举的结构
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns></returns>
        [return: NotNull]
        public static IReadOnlyDictionary<string, string> GetEnumStructString<TEnum>(
            this IEnumHelper enumHelper)
            where TEnum : Enum
        {
            return IEnumHelperExtensions.GetEnumStruct<TEnum, string>(enumHelper, t => t.BaseValueString);
        }

        /// <summary>
        /// 获取枚举的结构
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举对应的值类型</typeparam>
        /// <paramref name="enumHelper"></paramref>
        /// <returns></returns>
        [return: NotNull]
        public static IReadOnlyDictionary<string, TValue> GetEnumStruct<TEnum, TValue>(this IEnumHelper enumHelper)
            where TEnum : Enum
            where TValue : struct

        {
            return IEnumHelperExtensions.GetEnumStruct<TEnum, TValue>(enumHelper, t => (TValue)t.BaseValue);
        }

        #endregion 获取结构

        #region 获取枚举上的Attribute

        /// <summary>
        /// 获取枚举值上的默认第一个特性<para></para>
        /// 如果存在多个<typeparamref name="TAttribute"/>，取第一个
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TAttribute">枚举特性上面的类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="orderFunc">用于排序的委托</param>
        /// <returns></returns>
        public static TAttribute GetFirstAttribute<TEnum, TAttribute>(
            this IEnumHelper enumHelper,
            TEnum enumValue,
            Func<TAttribute, int> orderFunc)
            where TEnum : Enum
            where TAttribute : Attribute
        {
            //获取枚举结构数据
            EnumAttributeData[] fieldList = enumHelper.GetAllEnumAttributeData<TEnum>();

            //获取枚举值上面的特性集合
            Attribute[] enumValueAttributeArry = Array.Find(fieldList, t => enumValue.Equals(t.EnumValue))
                ?.AttributeArray;

            if (enumValueAttributeArry == null)
            {
                return null;
            }
            else
            {
                Type attributeType = typeof(TAttribute);
#pragma warning disable CS8604 // 可能的 null 引用参数。
                return enumValueAttributeArry
                    .Where(t => t.GetType() == attributeType)
                    .OrderBy(t => orderFunc(t as TAttribute))
                    .FirstOrDefault() as TAttribute;
#pragma warning restore CS8604 // 可能的 null 引用参数。
            }
        }

        /// <summary>
        /// 获取枚举值上的默认第一个特性<para></para>
        /// 如果存在多个<typeparamref name="TAttribute"/>，取第一个
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TAttribute">枚举特性上面的类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static TAttribute GetFirstAttribute<TEnum, TAttribute>(
            this IEnumHelper enumHelper,
            [NotNull] TEnum enumValue)
            where TEnum : Enum
            where TAttribute : Attribute
        {
            return IEnumHelperExtensions.GetFirstAttribute<TEnum, TAttribute>(
                enumHelper: enumHelper,
                enumValue: enumValue,
                orderFunc: _ => 1);
        }

        #endregion 获取枚举上的Attribute

        #region 获取枚举的基础信息

        /// <summary>
        /// 获取枚举值上面的结构信息
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="enumHelper"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static EnumAttributeData GetEnumAttributeData<TEnum>(
            this IEnumHelper enumHelper,
            TEnum enumValue)
            where TEnum : Enum
        {
            return enumHelper.GetAllEnumAttributeData<TEnum>()
                .First(t => t.EnumValue.GetHashCode() == enumValue.GetHashCode());//枚举的哈希值是枚举值对应的int
        }

        #endregion 获取枚举的基础信息
    }
}