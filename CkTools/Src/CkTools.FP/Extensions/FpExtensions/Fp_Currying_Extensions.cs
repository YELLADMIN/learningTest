namespace System
{
    /// <summary>
    /// 函数式扩展-柯里化
    /// </summary>
    public static class FP_Currying_Extensions
    {
        #region Action

        public static Func<T1, Action> Currying<T1>(
            this Action<T1> action)
        {
            return x => () => action(x);
        }

        public static Func<T1, Func<T2, Action>> Currying<T1, T2>(
            this Action<T1, T2> action)
        {
            return x => y => () => action(x, y);
        }

        public static Func<T1, Func<T2, Func<T3, Action>>> Currying<T1, T2, T3>(
            this Action<T1, T2, T3> action)
        {
            return x => y => z => () => action(x, y, z);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, Action>>>> Currying<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action)
        {
            return x => y => z => g => () => action(x, y, z, g);
        }

        #endregion Action

        #region Func

        public static Func<T1, Func<TResult>> Currying<T1, TResult>(
            this Func<T1, TResult> func)
        {
            return x => () => func(x);
        }

        public static Func<T1, Func<T2, Func<TResult>>> Currying<T1, T2, TResult>(
            this Func<T1, T2, TResult> func)
        {
            return x => y => () => func(x, y);
        }

        public static Func<T1, Func<T2, Func<T3, Func<TResult>>>> Currying<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> func)
        {
            return x => y => z => () => func(x, y, z);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<TResult>>>>> Currying<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func)
        {
            return x => y => z => g => () => func(x, y, z, g);
        }

        #endregion Func
    }
}