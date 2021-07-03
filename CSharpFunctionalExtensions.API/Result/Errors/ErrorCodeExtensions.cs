using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFunctionalExtensions.API {
    public static class ErrorCodeExtensions {
        static readonly IEnumerable<ushort> ClientArgumentErrorCodes = new List<ushort> {
            ErrorCode.INVALID_ARGUMENT.Value,
            ErrorCode.FAILED_PRECONDITION.Value,
            ErrorCode.OUT_OF_RANGE.Value,
            ErrorCode.NOT_FOUND.Value,
            ErrorCode.ALREADY_EXISTS.Value
        }.AsEnumerable();

        public static ErrorCode Combine(this IEnumerable<ErrorCode> errorCodes) {
            if (errorCodes == null || !errorCodes.Any()) throw new ArgumentException("At least one error code required.", nameof(errorCodes));

            var distinctErrorCodes = errorCodes.Distinct(new ErrorCodeEqualityComparer()).OrderBy(x => x.Priority).ToList();
            var firstPriorityErrorCode = distinctErrorCodes.First();

            if (distinctErrorCodes.Count() == 1)
                return firstPriorityErrorCode;

            return ClientArgumentErrorCodes.Contains(firstPriorityErrorCode.Value) 
                ? firstPriorityErrorCode  
                : ErrorCode.INVALID_ARGUMENT;
        }
    }
}
