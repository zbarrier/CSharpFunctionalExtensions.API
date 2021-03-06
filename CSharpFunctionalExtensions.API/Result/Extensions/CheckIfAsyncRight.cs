using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsRightOperand {
        public static Task<Result<T>> CheckIf<T>(this Result<T> result, bool condition, Func<T, Task<Result>> func) {
            if (condition)
                return result.Check(func);
            else
                return Task.FromResult(result);
        }

        public static Task<Result<T>> CheckIf<T, K>(this Result<T> result, bool condition, Func<T, Task<Result<K>>> func) {
            if (condition)
                return result.Check(func);
            else
                return Task.FromResult(result);
        }

        public static Task<Result<T>> CheckIf<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Task<Result>> func) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Check(func);
            else
                return Task.FromResult(result);
        }

        public static Task<Result<T>> CheckIf<T, K>(this Result<T> result, Func<T, bool> predicate, Func<T, Task<Result<K>>> func) {
            if (result.IsSuccess && predicate(result.Value))
                return result.Check(func);
            else
                return Task.FromResult(result);
        }
    }
}
