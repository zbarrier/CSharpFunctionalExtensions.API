using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        /// <summary>
        ///     This method should be used in linq queries. We recommend using Bind method.
        /// </summary>
        public static Task<Result<TR>> SelectMany<T, TK, TR>(
            this Result<T> result,
            Func<T, Task<Result<TK>>> func,
            Func<T, TK, TR> project) {
            return result
                .Bind(func)
                .Map(x => project(result.Value, x));
        }
    }
}