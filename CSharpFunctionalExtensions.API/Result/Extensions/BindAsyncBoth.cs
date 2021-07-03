using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsBothOperands {
        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static async Task<Result<K>> Bind<T, K>(this Task<Result<T>> resultTask, Func<T, Task<Result<K>>> func) {
            Result<T> result = await resultTask.DefaultAwait();
            return await result.Bind(func).DefaultAwait();
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static async Task<Result<K>> Bind<K>(this Task<Result> resultTask, Func<Task<Result<K>>> func) {
            Result result = await resultTask.DefaultAwait();
            return await result.Bind(func).DefaultAwait();
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static async Task<Result> Bind<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> func) {
            Result<T> result = await resultTask.DefaultAwait();
            return await result.Bind(func).DefaultAwait();
        }

        /// <summary>
        ///     Selects result from the return value of a given function. If the calling Result is a failure, a new failure result is returned instead.
        /// </summary>
        public static async Task<Result> Bind(this Task<Result> resultTask, Func<Task<Result>> func) {
            Result result = await resultTask.DefaultAwait();
            return await result.Bind(func).DefaultAwait();
        }
    }
}