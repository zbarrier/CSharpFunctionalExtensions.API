using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CSharpFunctionalExtensions.API {
    internal static class TaskExtensions {
        public static Task<T> AsCompletedTask<T>(this T obj) => Task.FromResult(obj);

        public static ConfiguredTaskAwaitable DefaultAwait(this System.Threading.Tasks.Task task) => task.ConfigureAwait(Result.DefaultConfigureAwait);

        public static ConfiguredTaskAwaitable<T> DefaultAwait<T>(this Task<T> task) => task.ConfigureAwait(Result.DefaultConfigureAwait);
    }
}