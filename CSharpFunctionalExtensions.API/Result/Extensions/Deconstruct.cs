namespace CSharpFunctionalExtensions.API
{
    public static partial class ResultExtensions
    {
        public static void Deconstruct(this Result result, out bool isSuccess, out bool isFailure)
        {
            isSuccess = result.IsSuccess;
            isFailure = result.IsFailure;
        }

        public static void Deconstruct(this Result result, out bool isSuccess, out bool isFailure, out Error error)
        {
            isSuccess = result.IsSuccess;
            isFailure = result.IsFailure;
            error = result.IsFailure ? result.Error : default;
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool isFailure)
        {
            isSuccess = result.IsSuccess;
            isFailure = result.IsFailure;
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool isFailure, out T value)
        {
            isSuccess = result.IsSuccess;
            isFailure = result.IsFailure;
            value = result.IsSuccess ? result.Value : default;
        }

        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out bool isFailure, out T value, out Error error)
        {
            isSuccess = result.IsSuccess;
            isFailure = result.IsFailure;
            value = result.IsSuccess ? result.Value : default;
            error = result.IsFailure ? result.Error : default;
        }
    }
}
