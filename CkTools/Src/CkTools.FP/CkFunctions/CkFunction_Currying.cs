using System;

namespace CkTools.FP
{
    /// <summary>
    /// 函数式功能
    /// </summary>
    public static partial class CkFunctions
    {
        #region Action

        public static Func<T1, Action> Currying<T1>(
             Action<T1> action)
        {
            return x => () => action(x);
        }

        public static Func<T1, Func<T2, Action>> Currying<T1, T2>(
             Action<T1, T2> action)
        {
            return x => y => () => action(x, y);
        }

        public static Func<T1, Func<T2, Func<T3, Action>>> Currying<T1, T2, T3>(
             Action<T1, T2, T3> action)
        {
            return x => y => z => () => action(x, y, z);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, Action>>>> Currying<T1, T2, T3, T4>(
             Action<T1, T2, T3, T4> action)
        {
            return x => y => z => g => () => action(x, y, z, g);
        }

        #endregion Action

        #region Func

        public static Func<T1, Func<TResult>> Currying<T1, TResult>(
             Func<T1, TResult> func)
        {
            return x => () => func(x);
        }

        public static Func<T1, Func<T2, Func<TResult>>> Currying<T1, T2, TResult>(
             Func<T1, T2, TResult> func)
        {
            return x => y => () => func(x, y);
        }

        public static Func<T1, Func<T2, Func<T3, Func<TResult>>>> Currying<T1, T2, T3, TResult>(
             Func<T1, T2, T3, TResult> func)
        {
            return x => y => z => () => func(x, y, z);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<TResult>>>>> Currying<T1, T2, T3, T4, TResult>(
             Func<T1, T2, T3, T4, TResult> func)
        {
            return x => y => z => g => () => func(x, y, z, g);
        }

        #endregion Func

        //todo: 后续还有东西没理解：https://zhuanlan.zhihu.com/p/94591842
    }
}