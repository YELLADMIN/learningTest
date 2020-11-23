using System.Diagnostics.CodeAnalysis;
using CkTools.FP;

namespace System
{
    /// <summary>
    /// 函数式扩展-管道
    /// </summary>
    public static class FP_Pipe_Extensions
    {
        #region 0个入参

        /// <summary>
        /// 管道
        /// </summary>
        /// <param name="sourceFunc"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Action Pipe(
            [NotNull] this Action sourceFunc,
            [NotNull] params Action[] actions)
        {
            return CkFunctions.Pipe(sourceFunc, actions);
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
            [NotNull] this Action<TInput> sourceFunc,
            [NotNull] params Action<TInput>[] actions)
        {
            return CkFunctions.Pipe(sourceFunc, actions);
        }

        #endregion 1个入参

        #region 返回Action

        #region 1个入参

        ///// <summary>
        ///// 管道 <para></para>
        ///// (a->b)->(b->void)->...  => (a->void) <para></para>
        ///// 示例:  (string->int)->(int->void)->...  => (string->void)
        ///// </summary>
        ///// <typeparam name="TInput"></typeparam>
        ///// <typeparam name="TResult"></typeparam>
        ///// <param name="sourceFunc"></param>
        ///// <param name="actions"></param>
        ///// <returns></returns>
        //public static Action<TInput> Pipe<TInput, TResult>(
        //    [NotNull] this Func<TInput, TResult> sourceFunc,
        //    [NotNull] params Action<TResult>[] actions)
        //{
        //    return CkFunctions.Pipe(sourceFunc, actions);
        //}

        #endregion 1个入参

        #endregion 返回Action

        #region 返回Func

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
          [NotNull] this Func<TInput, TCenter> sourceFunc,
          [NotNull] Func<TCenter, TResult> func)
        {
            return CkFunctions.Pipe(sourceFunc, func);
        }

        #endregion 返回Func
    }
}