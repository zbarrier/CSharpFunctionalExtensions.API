using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     If the calling Result is a success, a new success result is returned. Otherwise, creates a new failure result from the return value of a given function.
        /// </summary>
        public static async Task<Result> MapError(this Task<Result> resultTask, Func<Error, Error> errorFactory) {
            var result = await resultTask.DefaultAwait();

            return result.MapError(errorFactory);
        }

        /// <summary>
        ///     If the calling Result is a success, a new success result is returned. Otherwise, creates a new failure result from the return value of a given function.
        /// </summary>
        public static async Task<Result<T>> MapError<T>(this Task<Result<T>> resultTask, Func<Error, Error> errorFactory) {
            var result = await resultTask.DefaultAwait();

            return result.MapError(errorFactory);
        }
    }
}