using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsBothOperands {
        public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<Task<Result<T>>> func) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsFailure)
                return await func().DefaultAwait();

            return result;
        }

        public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<Task<Result>> func) {
            Result result = await resultTask.DefaultAwait();

            if (result.IsFailure)
                return await func().DefaultAwait();

            return result;
        }

        public static async Task<Result<T>> OnFailureCompensate<T>(this Task<Result<T>> resultTask, Func<Error, Task<Result<T>>> func) {
            Result<T> result = await resultTask.DefaultAwait();

            if (result.IsFailure)
                return await func(result.Error).DefaultAwait();

            return result;
        }
    }
}
