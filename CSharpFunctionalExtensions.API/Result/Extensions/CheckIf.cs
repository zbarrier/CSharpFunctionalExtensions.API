using System;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        public static Result<T> CheckIf<T>(this Result<T> result, bool condition, Func<T, Result> func) {
            if (condition)
                return result.Check(func);
            else
                return result;
        }

        public static Result<T> CheckIf<T, K>(this Result<T> result, bool condition, Func<T, Result<K>> func) {
            if (condition)
                return result.Check(func);
            else
                return result;
        }

        public static Result<T> CheckIf<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Result> func) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Check(func);
            else
                return result;
        }

        public static Result<T> CheckIf<T, K>(this Result<T> result, Func<T, bool> predicate, Func<T, Result<K>> func) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Check(func);
            else
                return result;
        }
    }
}