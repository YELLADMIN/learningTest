﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace System
{
    public static partial class FpObjectDoAsyncExtensions
    {
        /// <summary>
        /// 对<paramref name="input"/>执行一些操作
        /// </summary>
        /// <Value>
        /// <para><paramref name="input"/>：要处理的值 </para>
        /// <para><paramref name="isExecute"/>：判断是否执行，返回true为执行 </para>
        /// <para><paramref name="doAsyncFunc"/>：将要执行的处理 </para>
        /// </Value>
        /// <typeparam name="TInput">可传递任意类型</typeparam>
        /// <returns></returns>
        public static async Task<TInput> DoIfAsync<TInput>(
            this TInput input,
            [NotNull] Func<TInput, bool> isExecute,
            [NotNull] Func<TInput, Task> doAsyncFunc)
        {
            isExecute.CheckNullWithException(nameof(isExecute));
            doAsyncFunc.CheckNullWithException(nameof(doAsyncFunc));

            if (isExecute(input))
            {
                await doAsyncFunc(input);
            }

            return input;
        }

        /// <summary>
        /// 对<paramref name="input"/>执行一些操作
        /// </summary>
        /// <Value>
        /// <para><paramref name="input"/>：要处理的值 </para>
        /// <para><paramref name="isExecute"/>：判断是否执行，true为执行 </para>
        /// <para><paramref name="doAsyncFunc"/>：将要执行的处理 </para>
        /// </Value>
        /// <typeparam name="TInput">可传递任意类型</typeparam>
        /// <returns></returns>
        public static Task<TInput> DoIfAsync<TInput>(
            this TInput input,
            [NotNull] bool isExecute,
            [NotNull] Func<TInput, Task> doAsyncFunc)
        {
            return FpObjectDoAsyncExtensions.DoIfAsync(input, t => isExecute, doAsyncFunc);
        }
    }
}