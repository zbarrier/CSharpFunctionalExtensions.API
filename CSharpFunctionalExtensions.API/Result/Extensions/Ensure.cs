using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error errorMessage) {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Failure<T>(errorMessage);

            return result;
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Error> errorPredicate) {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Failure<T>(errorPredicate(result.Value));

            return result;
        }

        /// <summary>
        ///     Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        public static Result Ensure(this Result result, Func<bool> predicate, Error errorMessage) {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Failure(errorMessage);

            return result;
        }
    }
}
