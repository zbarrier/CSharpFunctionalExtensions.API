using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onFailure"/> function.
        /// </summary>
        public static K Match<K, T>(this Result<T> result, Func<T, K> onSuccess, Func<Error, K> onFailure) {
            return result.IsSuccess
                ? onSuccess(result.Value)
                : onFailure(result.Error);
        }

        /// <summary>
        ///     Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onFailure"/> function.
        /// </summary>
        public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure) {
            return result.IsSuccess
                ? onSuccess()
                : onFailure(result.Error);
        }

        /// <summary>
        ///     Invokes the given <paramref name="onSuccess"/> action if the calling Result is a success. Otherwise, it invokes the given <paramref name="onFailure"/> action.
        /// </summary>
        public static void Match<T>(this Result<T> result, Action<T> onSuccess, Action<Error> onFailure) {
            if (result.IsSuccess)
                onSuccess(result.Value);
            else
                onFailure(result.Error);
        }

        /// <summary>
        ///     Invokes the given <paramref name="onSuccess"/> action if the calling Result is a success. Otherwise, it invokes the given <paramref name="onFailure"/> action.
        /// </summary>
        public static void Match(this Result result, Action onSuccess, Action<Error> onFailure) {
            if (result.IsSuccess)
                onSuccess();
            else
                onFailure(result.Error);
        }
    }
}
