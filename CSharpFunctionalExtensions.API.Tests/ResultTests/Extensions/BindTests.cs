using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class BindTests : BindTestsBase {
        [Fact]
        public void Bind_returns_failure_and_does_not_execute_func() {
            Result input = Result.Failure(ErrorMessage);

            Result output = input.Bind(GetResult);

            AssertFailure(output);
        }

        [Fact]
        public void Bind_selects_new_result() {
            Result input = Result.Success();

            Result output = input.Bind(GetResult);

            AssertSuccess(output);
        }

        [Fact]
        public void Bind_T_returns_failure_and_does_not_execute_func() {
            Result<T> input = Result.Failure<T>(ErrorMessage);

            Result output = input.Bind(GetResult_WithParam);

            AssertFailure(output);
        }

        [Fact]
        public void Bind_T_selects_new_result() {
            Result<T> input = Result.Success(T.Value);

            Result output = input.Bind(GetResult_WithParam);

            funcParam.Should().Be(T.Value);
            AssertSuccess(output);
        }

        [Fact]
        public void Bind_K_returns_failure_and_does_not_execute_func() {
            Result input = Result.Failure(ErrorMessage);

            Result<K> output = input.Bind(GetResult_K);

            AssertFailure(output);
        }

        [Fact]
        public void Bind_K_selects_new_result() {
            Result input = Result.Success();

            Result<K> output = input.Bind(GetResult_K);

            AssertSuccess(output);
        }

        [Fact]
        public void Bind_T_K_returns_failure_and_does_not_execute_func() {
            Result<T> input = Result.Failure<T>(ErrorMessage);

            Result<K> output = input.Bind(GetResult_K_WithParam);

            AssertFailure(output);
        }

        [Fact]
        public void Bind_T_K_selects_new_result() {
            Result<T> input = Result.Success(T.Value);

            Result<K> output = input.Bind(GetResult_K_WithParam);

            funcParam.Should().Be(T.Value);
            AssertSuccess(output);
        }
    }
}
