using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API
{
    public static partial class AsyncResultExtensionsLeftOperand
    {
        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Action action)
        {
            Result<T> result = await resultTask.DefaultAwait();
            return result.OnFailure(action);
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result> OnFailure(this Task<Result> resultTask, Action action)
        {
            Result result = await resultTask.DefaultAwait();
            return result.OnFailure(action);
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Action<Error> action)
        {
            Result<T> result = await resultTask.DefaultAwait();
            return result.OnFailure(action);
        }

        /// <summary>
        ///     Executes the given action if the calling result is a failure. Returns the calling result.
        /// </summary>
        public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<Error> action)
        {
            Result result = await resultTask.DefaultAwait();
            return result.OnFailure(action);
        }
    }
}
