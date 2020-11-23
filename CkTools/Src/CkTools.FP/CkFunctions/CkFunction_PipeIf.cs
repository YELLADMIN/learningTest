using System;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        #region Action - 0入参 0出参

        #endregion Action - 0入参 0出参

        #region Action - 1入参 0出参

        #endregion Action - 1入参 0出参

        #region Func - 0入参 1出参

        #endregion Func - 0入参 1出参

        #region Fun - 1入参 1出参

        /// <summary>
        /// 管道
        /// </summary>
        /// <Value>
        /// <para><paramref name="isExecute"/>：判断是否执行，返回true为执行 </para>
        /// <para><paramref name="func"/>：将要执行的处理 </para>
        /// </Value>
        /// <typeparam name="TInput">可传递任意类型</typeparam>
        /// <returns></returns>
        public static Func<TInput, TInput> PipeIf<TInput>(
            [NotNull] Func<TInput, TInput> func,
            [NotNull] Func<TInput, bool> isExecute)
        {
            func.CheckNullWithException(nameof(func));
            isExecute.CheckNullWithException(nameof(isExecute));
            return CkFunctions.If<TInput, TInput>(t => t, func, isExecute);
        }

        #endregion Fun - 1入参 1出参
    }
}