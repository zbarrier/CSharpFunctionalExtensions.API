using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public partial struct Result {
        /// <summary>
        ///     Creates a result whose success/failure reflects the supplied condition. Opposite of SuccessIf().
        /// </summary>
        public static Result FailureIf(bool isFailure, Error error)
            => SuccessIf(!isFailure, error);

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of SuccessIf().
        /// </summary>
        public static Result FailureIf(Func<bool> failurePredicate, Error error)
            => SuccessIf(!failurePredicate(), error);

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of SuccessIf().
        /// </summary>
        public static async Task<Result> FailureIf(Func<Task<bool>> failurePredicate, Error error) {
            bool isFailure = await failurePredicate().DefaultAwait();
            return SuccessIf(!isFailure, error);
        }

        /// <summary>
        ///     Creates a result whose success/failure reflects the supplied condition. Opposite of SuccessIf().
        /// </summary>
        public static Result<T> FailureIf<T>(bool isFailure, T value, Error error)
            => SuccessIf(!isFailure, value, error);

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of SuccessIf().
        /// </summary>
        public static Result<T> FailureIf<T>(Func<bool> failurePredicate, T value, Error error)
            => SuccessIf(!failurePredicate(), value, error);

        /// <summary>
        ///     Creates a result whose success/failure depends on the supplied predicate. Opposite of SuccessIf().
        /// </summary>
        public static async Task<Result<T>> FailureIf<T>(Func<Task<bool>> failurePredicate, T value, Error error) {
            bool isFailure = await failurePredicate().DefaultAwait();
            return SuccessIf(!isFailure, value, error);
        }
    }
}
