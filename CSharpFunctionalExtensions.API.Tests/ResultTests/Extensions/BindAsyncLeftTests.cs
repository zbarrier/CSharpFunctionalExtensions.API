using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class BindAsyncLeftTests : BindTestsBase {
        [Fact]
        public void Bind_AsyncLeft_returns_failure_and_does_not_execute_func() {
            Result input = Result.Failure(ErrorMessage);

            Result output = input.AsTask().Bind(GetResult).Result;

            AssertFailure(output);
        }

        [Fact]
        public void Bind_AsyncLeft_selects_new_result() {
            Result input = Result.Success();

            Result output = input.AsTask().Bind(GetResult).Result;

            AssertSuccess(output);
        }

        [Fact]
        public void Bind_T_AsyncLeft_returns_failure_and_does_not_execute_func() {
            Result<T> input = Result.Failure<T>(ErrorMessage);

            Result output = input.AsTask().Bind(GetResult_WithParam).Result;

            AssertFailure(output);
        }

        [Fact]
        public void Bind_T_AsyncLeft_selects_new_result() {
            Result<T> input = Result.Success(T.Value);

            Result output = input.AsTask().Bind(GetResult_WithParam).Result;

            funcParam.Should().Be(T.Value);
            AssertSuccess(output);
        }

        [Fact]
        public void Bind_K_AsyncLeft_returns_failure_and_does_not_execute_func() {
            Result input = Result.Failure(ErrorMessage);

            Result<K> output = input.AsTask().Bind(GetResult_K).Result;

            AssertFailure(output);
        }

        [Fact]
        public void Bind_K_AsyncLeft_selects_new_result() {
            Result input = Result.Success();

            Result<K> output = input.AsTask().Bind(GetResult_K).Result;

            AssertSuccess(output);
        }

        [Fact]
        public void Bind_T_K_AsyncLeft_returns_failure_and_does_not_execute_func() {
            Result<T> input = Result.Failure<T>(ErrorMessage);

            Result<K> output = input.AsTask().Bind(GetResult_K_WithParam).Result;

            AssertFailure(output);
        }

        [Fact]
        public void Bind_T_K_AsyncLeft_selects_new_result() {
            Result<T> input = Result.Success(T.Value);

            Result<K> output = input.AsTask().Bind(GetResult_K_WithParam).Result;

            funcParam.Should().Be(T.Value);
            AssertSuccess(output);
        }
    }
}
