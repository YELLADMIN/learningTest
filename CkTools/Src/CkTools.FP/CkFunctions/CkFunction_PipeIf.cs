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
        /// 管道 <para></para>
        /// a->(a->bool)->(a->a) => a <para></para>
        /// 示例： string->(string->bool)->(string->string) => string
        /// </summary>
        /// <Value>
        /// <para><paramref name="input"/>：要处理的值 </para>
        /// <para><paramref name="isExecute"/>：判断是否执行，返回true为执行 </para>
        /// <para><paramref name="func"/>：将要执行的处理 </para>
        /// </Value>
        /// <typeparam name="TInput">可传递任意类型</typeparam>
        /// <returns></returns>
        public static Func<TInput, TInput> PipeIf<TInput>(
            this TInput input,
            [NotNull] Func<TInput, TInput> func,
            [NotNull] Func<TInput, bool> isExecute)
        {
            Func<TInput, TInput> ifMethod = CkFunctions.IfCurrying<TInput>()(t => t)(func)(isExecute)();

            return ifMethod;
        }
    }
}