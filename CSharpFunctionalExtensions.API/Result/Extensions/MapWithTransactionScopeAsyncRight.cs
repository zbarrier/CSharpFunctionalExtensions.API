using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class ResultExtensions {
        public static Task<Result<K>> MapWithTransactionScope<T, K>(this Result<T> self, Func<T, Task<K>> f)
            => WithTransactionScope(() => self.Map(f));

        public static Task<Result<K>> MapWithTransactionScope<K>(this Result self, Func<Task<K>> f)
            => WithTransactionScope(() => self.Map(f));
    }
}
