using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsRightOperand {
        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Task> func) {
            if (result.IsFailure) {
                await func().DefaultAwait();
            }

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result> OnFailure(this Result result, Func<Task> func) {
            if (result.IsFailure) {
                await func().DefaultAwait();
            }

            return result;
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Error, Task> func) {
            if (result.IsFailure) {
                await func(result.Error).DefaultAwait();
            }

            return result;
        }
    }
}
