using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.API.Tests
{
    internal static class TaskExtensions
    {
        public static Task<T> AsTask<T>(this T obj) => Task.FromResult(obj);
    }
}