using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public partial struct Result {
        /// <summary>
        ///     Creates a result whose success/failure reflects the supplied condition. Opposite of FailureIf().
        /// </summary>
        public static Result SuccessIf(bool isSuccess, Error error) {
            return isSuccess
                ? Success()
                : Failure(error);
        }

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of FailureIf().
        /// </summary>
        public static Result SuccessIf(Func<bool> predicate, Error error) {
            return SuccessIf(predicate(), error);
        }

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of FailureIf().
        /// </summary>
        public static async Task<Result> SuccessIf(Func<Task<bool>> predicate, Error error) {
            bool isSuccess = await predicate().DefaultAwait();
            return SuccessIf(isSuccess, error);
        }

        /// <summary>
        ///     Creates a result whose success/failure reflects the supplied condition. Opposite of FailureIf().
        /// </summary>
        public static Result<T> SuccessIf<T>(bool isSuccess, T value, Error error) {
            return isSuccess
                ? Success(value)
                : Failure<T>(error);
        }

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of FailureIf().
        /// </summary>
        public static Result<T> SuccessIf<T>(Func<bool> predicate, T value, Error error) {
            return SuccessIf(predicate(), value, error);
        }

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of FailureIf().
        /// </summary>
        public static async Task<Result<T>> SuccessIf<T>(Func<Task<bool>> predicate, T value, Error error) {
            bool isSuccess = await predicate().DefaultAwait();
            return SuccessIf(isSuccess, value, error);
        }
    }
}
