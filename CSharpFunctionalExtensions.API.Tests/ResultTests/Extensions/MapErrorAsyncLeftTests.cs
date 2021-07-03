using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class MapErrorAsyncLeftTests : TestBase {
        [Fact]
        public async Task MapError_returns_success() {
            Task<Result> result = Result.Success().AsTask();
            var invocations = 0;

            Result actual = await result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_returns_new_failure() {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
            var invocations = 0;

            Result actual = await result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Message.Should().Be($"{ErrorMessage} {ErrorMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_T_returns_success() {
            Task<Result<T>> result = Result.Success(T.Value).AsTask();
            var invocations = 0;

            Result<T> actual = await result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            actual.Value.Should().Be(T.Value);
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_T_returns_new_failure() {
            Task<Result<T>> result = Result.Failure<T>(ErrorMessage).AsTask();
            var invocations = 0;

            Result<T> actual = await result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Message.Should().Be($"{ErrorMessage} {ErrorMessage}");
            invocations.Should().Be(1);
        }
    }
}