using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     Passes the result to the given function (regardless of success/failure state) to yield a final output value.
        /// </summary>
        public static T Finally<T>(this Result result, Func<Result, T> func)
            => func(result);

        /// <summary>
        ///     Passes the result to the given function (regardless of success/failure state) to yield a final output value.
        /// </summary>
        public static K Finally<T, K>(this Result<T> result, Func<Result<T>, K> func)
            => func(result);
    }
}