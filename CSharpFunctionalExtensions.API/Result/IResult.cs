namespace CSharpFunctionalExtensions.API
{
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess { get; }
        Error Error { get; }
    }

    public interface IValue<out T>
    {
        T Value { get; }
    }

    public interface IResult<out T> : IResult, IValue<T> {
    }
}
