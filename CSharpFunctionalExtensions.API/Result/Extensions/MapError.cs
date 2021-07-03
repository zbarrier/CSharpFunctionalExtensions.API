﻿using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     If the calling Result is a success, a new success result is returned. Otherwise, creates a new failure result from the return value of a given function.
        /// </summary>
        public static Result MapError(this Result result, Func<Error, Error> errorFactory) {
            if (result.IsFailure)
                return Result.Failure(errorFactory(result.Error));

            return Result.Success();
        }

        /// <summary>
        ///     If the calling Result is a success, a new success result is returned. Otherwise, creates a new failure result from the return value of a given function.
        /// </summary>
        public static Result<T> MapError<T>(this Result<T> result, Func<Error, Error> errorFactory) {
            if (result.IsFailure)
                return Result.Failure<T>(errorFactory(result.Error));

            return Result.Success(result.Value);
        }
    }
}