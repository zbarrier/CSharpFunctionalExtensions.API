using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsLeftOperand {
        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Error errorMessage) {
            Result<T> result = await resultTask.DefaultAwait();
            return result.Ensure(predicate, errorMessage);
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Error> errorPredicate) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsFailure)
                return result;

            return result.Ensure(predicate, errorPredicate(result.Value));
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Task<Error>> errorPredicate) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsFailure)
                return result;

            return result.Ensure(predicate, await errorPredicate(result.Value));
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static async Task<Result> Ensure(this Task<Result> resultTask, Func<bool> predicate, Error errorMessage) {
            Result result = await resultTask.DefaultAwait();
            return result.Ensure(predicate, errorMessage);
        }
    }
}
