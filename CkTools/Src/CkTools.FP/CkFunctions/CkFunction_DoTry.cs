﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        #region Action - 0入参 0出参

        /// <summary>
        /// 管道,返回一个包含异常处理的函数
        /// </summary>
        /// <Value>
        /// <para><paramref name="sourceFunc"/>：要附加try操作的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数 </para>
        /// </Value>
        /// <returns></returns>
        public static Action DoTry(
            [NotNull] Action sourceFunc,
            [NotNull] Action<Exception> handler)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            handler.CheckNullWithException(nameof(handler));

            return CkFunctions.Try(sourceFunc, handler);
        }

        #endregion Action - 0入参 0出参

        #region Action - 1入参 0出参

        /// <summary>
        /// 管道,返回一个包含异常处理的函数
        /// </summary>
        /// <Value>
        /// <para><paramref name="sourceFunc"/>：要附加try操作的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数 </para>
        /// </Value>
        /// <typeparam name="TInput"></typeparam>
        /// <returns></returns>
        public static Action<TInput> DoTry<TInput>(
            [NotNull] Action<TInput> sourceFunc,
            [NotNull] Action<TInput, Exception> handler)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            handler.CheckNullWithException(nameof(handler));

            return CkFunctions.Try(sourceFunc, handler);
        }

        #endregion Action - 1入参 0出参
    }
}