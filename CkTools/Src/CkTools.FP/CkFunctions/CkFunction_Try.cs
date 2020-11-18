using System;
using System.Diagnostics.CodeAnalysis;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        #region Action

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="action"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数</para>
        /// </Value>
        /// <returns></returns>
        public static Action Try(
            [NotNull] Action action,
            [NotNull] Action<Exception> handler)
        {
            action.CheckNullWithException(nameof(action));
            handler.CheckNullWithException(nameof(handler));
            return () => { try { action(); } catch (Exception ex) { handler(ex); } };
        }

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="action"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数</para>
        /// </Value>
        /// <typeparam name="TInput">输入参数类型</typeparam>
        /// <returns></returns>
        public static Action<TInput> Try<TInput>(
            [NotNull] Action<TInput> action,
            [NotNull] Action<TInput, Exception> handler)
        {
            action.CheckNullWithException(nameof(action));
            handler.CheckNullWithException(nameof(handler));
            return input => { try { action(input); } catch (Exception ex) { handler(input, ex); } };
        }

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="action"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数</para>
        /// </Value>
        /// <typeparam name="TInput">输入参数类型</typeparam>
        /// <returns></returns>
        public static Action<TInput> Try<TInput>(
            Action<TInput> action,
            Action<Exception> handler)
        {
            return CkFunctions.Try(action, (input, ex) => handler(ex));
        }

        #endregion Action

        #region Func - 0入参

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="func"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数,需要返回发生异常时的返回值</para>
        /// </Value>
        /// <typeparam name="TOutput">输出类型参数</typeparam>
        /// <returns></returns>
        public static Func<TOutput> Try<TOutput>(
            [NotNull] Func<TOutput> func,
            [NotNull] Func<Exception, TOutput> handler)
        {
            func.CheckNullWithException(nameof(func));
            handler.CheckNullWithException(nameof(handler));
            return () => { try { return func(); } catch (Exception ex) { return handler(ex); } };
        }

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="func"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数,需要返回发生异常时的返回值</para>
        /// </Value>
        /// <typeparam name="TOutput">输出类型参数</typeparam>
        /// <returns></returns>
        public static Func<TOutput> TryWithThrow<TOutput>(
            [NotNull] Func<TOutput> func,
            [NotNull] Action<Exception> handler)
        {
            return CkFunctions.Try(func, ex => { handler(ex); throw ex; });
        }

        #endregion Func - 0入参

        /// <summary>
        /// Try
        /// </summary>
        /// <Value>
        /// <para><paramref name="func"/>：要执行的函数 </para>
        /// <para><paramref name="handler"/>：异常处理函数,需要返回一个值</para>
        /// </Value>
        /// <typeparam name="TInput">输入类型参数</typeparam>
        /// <typeparam name="TOutput">输出类型参数</typeparam>
        /// <returns></returns>
        public static Func<TInput, TOutput> Try<TInput, TOutput>(
            [NotNull] Func<TInput, TOutput> func,
            [NotNull] Func<TInput, Exception, TOutput> handler)
        {
            func.CheckNullWithException(nameof(func));
            handler.CheckNullWithException(nameof(handler));
            return input => { try { return func(input); } catch (Exception ex) { return handler(input, ex); } };
        }
    }
}