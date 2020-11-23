using System;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunction_PipeTryIf
    {
        #region Func - 0入参 1出参

        public static Func<TOutput> PipeTryIf<TOutput>(
            [NotNull] Func<TOutput> sourceFunc,
            [NotNull] Func<Exception, TOutput> handler)
        {
            //sourceFunc.CheckNullWithException(nameof(sourceFunc));
            //handler.CheckNullWithException(nameof(handler));

            //return CkFunctions.Try(sourceFunc, handler);
        }

        #endregion Func - 0入参 1出参

        #region Func - 1出参 1出参

        #endregion Func - 1出参 1出参
    }
}