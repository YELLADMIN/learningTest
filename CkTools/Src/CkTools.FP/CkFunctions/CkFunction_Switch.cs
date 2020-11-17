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
        /// 选择器 <para></para>
        /// (a->b)->(b->byte)->(b->c)->...=> (a->c) <para></para>
        /// 示例： (string->int)->(int->byte)->(int->bool).... => (string->bool)
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TCenter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceFunc"></param>
        /// <param name="selector">选择器，选择执行第几个函数,索引从0开始</param>
        /// <param name="funcArray">函数数组</param>
        /// <returns></returns>
        public static Func<TInput, TResult> Switch<TInput, TCenter, TResult>(
          [NotNull] Func<TInput, TCenter> sourceFunc,
          [NotNull] Func<TCenter, byte> selector,
          [NotNull] params Func<TCenter, TResult>[] funcArray)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            selector.CheckNullWithException(nameof(selector));
            funcArray.CheckNullWithException(nameof(funcArray));

            return t => sourceFunc(t).Pipe(c => funcArray[selector(c)](c));
        }
    }
}