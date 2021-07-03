using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API {
    public static partial class AsyncResultExtensionsLeftOperand {
        public static async Task<Result> Combine(this IEnumerable<Task<Result>> tasks, string errorMessageSeparator = null) {
            Result[] results = await Task.WhenAll(tasks).DefaultAwait();
            return results.Combine(errorMessageSeparator);
        }

        public static async Task<Result<IEnumerable<T>>> Combine<T>(this IEnumerable<Task<Result<T>>> tasks, string errorMessageSeparator = null) {
            Result<T>[] results = await Task.WhenAll(tasks).DefaultAwait();
            return results.Combine(errorMessageSeparator);
        }

        public static async Task<Result> Combine(this Task<IEnumerable<Result>> task, string errorMessageSeparator = null) {
            IEnumerable<Result> results = await task.DefaultAwait();
            return results.Combine(errorMessageSeparator);
        }

        public static async Task<Result<IEnumerable<T>>> Combine<T>(this Task<IEnumerable<Result<T>>> task, string errorMessageSeparator = null) {
            IEnumerable<Result<T>> results = await task.DefaultAwait();
            return results.Combine(errorMessageSeparator);
        }

        public static async Task<Result> Combine(this Task<IEnumerable<Task<Result>>> task, string errorMessageSeparator = null) {
            IEnumerable<Task<Result>> tasks = await task.DefaultAwait();
            return await tasks.Combine(errorMessageSeparator).DefaultAwait();
        }

        public static async Task<Result<IEnumerable<T>>> Combine<T>(this Task<IEnumerable<Task<Result<T>>>> task, string errorMessageSeparator = null) {
            IEnumerable<Task<Result<T>>> tasks = await task.DefaultAwait();
            return await tasks.Combine(errorMessageSeparator).DefaultAwait();
        }

        public static async Task<Result<K>> Combine<T, K>(this IEnumerable<Task<Result<T>>> tasks, Func<IEnumerable<T>, K> composer, string errorMessageSeparator = null) {
            IEnumerable<Result<T>> results = await Task.WhenAll(tasks).DefaultAwait();
            return results.Combine(composer, errorMessageSeparator);
        }

        public static async Task<Result<K>> Combine<T, K>(this Task<IEnumerable<Task<Result<T>>>> task, Func<IEnumerable<T>, K> composer, string errorMessageSeparator = null) {
            IEnumerable<Task<Result<T>>> tasks = await task.DefaultAwait();
            return await tasks.Combine(composer, errorMessageSeparator).DefaultAwait();
        }
    }
}
