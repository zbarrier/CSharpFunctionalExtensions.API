using System;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class ToStringTests {
        [Fact]
        public void ToString_returns_failure_with_error_when_failure() {
            var subject = Result.Failure(NewError("BigError"));
            Assert.Equal("Failure(INVALID_ARGUMENT, BigError)", subject.ToString());
        }

        [Fact]
        public void ToString_returns_failure_with_generic_result_error_when_failure() {
            var subject = Result.Failure<string>(NewError("BigError"));
            Assert.Equal("Failure(INVALID_ARGUMENT, BigError)", subject.ToString());
        }

        [Fact]
        public void ToString_returns_success() {
            var subject = Result.Success();
            Assert.Equal("Success", subject.ToString());
        }

        [Fact]
        public void ToString_returns_success_with_generic_result() {
            var subject = Result.Success(1);
            Assert.Equal("Success(1)", subject.ToString());
        }


        Error NewError(string message) => new Error(ErrorCode.INVALID_ARGUMENT, message);
    }
}