using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class MapErrorTests : TestBase {
        [Fact]
        public void MapError_returns_success() {
            Result result = Result.Success();
            var invocations = 0;

            Result actual = result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public void MapError_returns_new_failure() {
            Result result = Result.Failure(ErrorMessage);
            var invocations = 0;

            Result actual = result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Message.Should().Be($"{ErrorMessage} {ErrorMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public void MapError_T_returns_success() {
            Result<T> result = Result.Success(T.Value);
            var invocations = 0;

            Result<T> actual = result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            actual.Value.Should().Be(T.Value);
            invocations.Should().Be(0);
        }

        [Fact]
        public void MapError_T_returns_new_failure() {
            Result<T> result = Result.Failure<T>(ErrorMessage);
            var invocations = 0;

            Result<T> actual = result.MapError(error => {
                invocations++;
                return NewError($"{error} {error}");
            });

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Message.Should().Be($"{ErrorMessage} {ErrorMessage}");
            invocations.Should().Be(1);
        }
    }
}