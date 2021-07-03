using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public partial struct Result {
        private static readonly Func<Exception, Error> DefaultTryErrorHandler = exc => new Error(ErrorCode.UNKNOWN, exc.Message);

        /// <summary>
        ///     Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.
        /// </summary>
        public static Result Try(Action action, Func<Exception, Error> errorHandler = null) {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try {
                action();
                return Success();
            }
            catch (Exception exc) {
                Error error = errorHandler(exc);
                return Failure(error);
            }
        }

        /// <summary>
        ///     Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.
        /// </summary>
        public static async Task<Result> Try(Func<Task> action, Func<Exception, Error> errorHandler = null) {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try {
                await action().DefaultAwait();
                return Success();
            }
            catch (Exception exc) {
                Error error = errorHandler(exc);
                return Failure(error);
            }
        }

        /// <summary>
        ///     Attempts to execute the supplied function. Returns a Result indicating whether the function executed successfully.
        ///     If the function executed successfully, the result contains its return value.
        /// </summary>
        public static Result<T> Try<T>(Func<T> func, Func<Exception, Error> errorHandler = null) {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try {
                return Success(func());
            }
            catch (Exception exc) {
                Error error = errorHandler(exc);
                return Failure<T>(error);
            }
        }

        /// <summary>
        ///     Attempts to execute the supplied function. Returns a Result indicating whether the function executed successfully.
        ///     If the function executed successfully, the result contains its return value.
        /// </summary>
        public static async Task<Result<T>> Try<T>(Func<Task<T>> func, Func<Exception, Error> errorHandler = null) {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try {
                var result = await func().DefaultAwait();
                return Success(result);
            }
            catch (Exception exc) {
                Error error = errorHandler(exc);
                return Failure<T>(error);
            }
        }
    }
}
