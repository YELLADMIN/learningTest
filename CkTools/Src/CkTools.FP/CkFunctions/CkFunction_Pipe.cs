using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        #region 0个入参

        /// <summary>
        /// 管道
        /// </summary>
        /// <param name="sourceFunc"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Action Pipe(
            [NotNull] Action sourceFunc,
            [NotNull] params Action[] actions)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            actions.CheckNullWithException(nameof(actions));

            actions.For(t => sourceFunc += t);
            return sourceFunc;
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <param name="sourceFunc"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Func<TOutput> Pipe<TOutput>(
            [NotNull] Func<TOutput> sourceFunc,
            [NotNull] params Action<TOutput>[] actions)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            actions.CheckNullWithException(nameof(actions));

            return () =>
            {
                TOutput result = sourceFunc();
                actions.For(t => t(result));
                return result;
            };
        }

        #endregion 0个入参

        #region 1个入参

        /// <summary>
        /// 管道
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="sourceFunc"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Action<TInput> Pipe<TInput>(
            [NotNull] Action<TInput> sourceFunc,
            [NotNull] params Action<TInput>[] actions)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            actions.CheckNullWithException(nameof(actions));

            return input =>
            {
                sourceFunc(input);
                actions.For(item => item(input));
            };
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceFunc"></param>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public static Func<TInput, TResult> Pipe<TInput, TResult>(
            [NotNull] Func<TInput, TResult> sourceFunc,
            [NotNull] params Func<TResult, TResult>[] funcs)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            funcs.CheckNullWithException(nameof(funcs));

            return t =>
            {
                TResult tempResult = sourceFunc(t);
                funcs.For(item => tempResult = item(tempResult));
                return tempResult;
            };
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceFunc"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Func<TInput, TResult> Pipe<TInput, TResult>(
            [NotNull] Func<TInput, TResult> sourceFunc,
            [NotNull] params Action<TResult>[] actions)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            actions.CheckNullWithException(nameof(actions));

            return input =>
            {
                TResult tempResult = sourceFunc(input);
                actions.For(item => item(tempResult));

                return tempResult;
            };
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TCenter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceFunc"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<TInput, TResult> Pipe<TInput, TCenter, TResult>(
          [NotNull] Func<TInput, TCenter> sourceFunc,
          [NotNull] Func<TCenter, TResult> func)
        {
            sourceFunc.CheckNullWithException(nameof(sourceFunc));
            func.CheckNullWithException(nameof(func));
            return t => func(sourceFunc(t));
        }

        #endregion 1个入参
    }
}