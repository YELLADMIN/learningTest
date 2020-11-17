using System;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        /// <summary>
        /// 结合
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TCenter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func1"></param>
        /// <param name="func2"></param>
        /// <returns></returns>
        public static Func<TInput, TResult> Compose<TInput, TCenter, TResult>(
            [NotNull] Func<TCenter, TResult> func2,
            [NotNull] Func<TInput, TCenter> func1)
        {
            return CkFunctions.Pipe(func1, func2);
        }
    }
}