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
        /// If
        /// </summary>
        /// <Value>
        /// <para><paramref name="exp2"/>：为false时执行的函数 </para>
        /// <para><paramref name="exp1"/> 为true时执行的函数 </para>
        /// <para><paramref name="isExecute"/>：指示如何判断的函数 </para>
        /// </Value>
        /// <typeparam name="TInput"></typeparam>
        /// <returns></returns>
        public static Func<TInput, TInput> If<TInput>(
            [NotNull] Func<TInput, TInput> exp2,
            [NotNull] Func<TInput, TInput> exp1,
            [NotNull] Func<TInput, bool> isExecute)
        {
            exp2.CheckNullWithException(nameof(exp2));
            exp1.CheckNullWithException(nameof(exp1));
            isExecute.CheckNullWithException(nameof(isExecute));

            return input => isExecute(input)
            ? exp1(input)
            : exp2(input);
        }
    }
}