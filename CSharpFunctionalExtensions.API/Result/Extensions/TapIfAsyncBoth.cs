using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsBothOperands {
        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Task<Result> TapIf(this Task<Result> resultTask, bool condition, Func<Task> func) {
            if (condition)
                return resultTask.Tap(func);
            else
                return resultTask;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Func<Task> func) {
            if (condition)
                return resultTask.Tap(func);
            else
                return resultTask;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, bool condition, Func<T, Task> func) {
            if (condition)
                return resultTask.Tap(func);
            else
                return resultTask;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<Task> func) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsSuccess && predicate(result.Value))
                return await result.Tap(func).DefaultAwait();
            else
                return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a success and condition is true. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> TapIf<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Task> func) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsSuccess && predicate(result.Value))
                return await result.Tap(func).DefaultAwait();
            else
                return result;
        }
    }
}
