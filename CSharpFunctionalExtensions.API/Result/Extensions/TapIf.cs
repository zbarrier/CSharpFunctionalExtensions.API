using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result TapIf(this Result result, bool condition, Action action) {
            if (condition)
                return result.Tap(action);
            else
                return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, bool condition, Action action) {
            if (condition)
                return result.Tap(action);
            else
                return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, bool condition, Action<T> action) {
            if (condition)
                return result.Tap(action);
            else
                return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Action action) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Tap(action);
            else
                return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Result<T> TapIf<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Tap(action);
            else
                return result;
        }
    }
}
