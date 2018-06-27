using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionMethods
    {
        public static TResult Transform<TInput, TResult> (this TInput @this, Func<TInput, TResult> func) => func(@this);

        public async static Task<TResult> TransformAsync<TInput, TResult>(this TInput @this, Func<TInput, Task<TResult>> func) => await func(@this);

        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        public static T When<T>(this T @this, Func<bool> condition, Func<T, T> func) =>
            condition() ? func(@this) : @this;

        public static Task<T> WhenAsync<T>(this T @this, Func<bool> condition, Func<T, Task<T>> func) =>
           condition() ? func(@this) : Task.Factory.StartNew(() => @this);
    }
}
