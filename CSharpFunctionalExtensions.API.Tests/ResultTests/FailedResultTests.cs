using System;
using FluentAssertions;
using Xunit;


namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class FailedResultTests {
        [Fact]
        public void Can_create_a_non_generic_version() {
            Result result = Result.Failure(NewError("Error message"));

            result.Error.Message.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version() {
            Result<MyClass> result = Result.Failure<MyClass>(NewError("Error message"));

            result.Error.Message.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Cannot_access_Value_property() {
            Result<MyClass> result = Result.Failure<MyClass>(NewError("Error message"));

            Action action = () => { MyClass myClass = result.Value; };

            action.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Include_Error_in_Exception_message() {
            Result<MyClass> result = Result.Failure<MyClass>(NewError("Error message"));

            Action action = () => { MyClass myClass = result.Value; };

            action.Should().Throw<ResultFailureException>()
                .WithMessage("You attempted to access the Value property for a failed result. A failed result has no Value. The error was: INVALID_ARGUMENT, Error message");
        }

        [Fact]
        public void Cannot_create_without_error_message() {
            Action action1 = () => { Result.Failure(null); };
            Action action2 = () => { Result.Failure(NewError(string.Empty)); };
            Action action3 = () => { Result.Failure<MyClass>(null); };
            Action action4 = () => { Result.Failure<MyClass>(NewError(string.Empty)); };

            action1.Should().Throw<ArgumentNullException>();
            action2.Should().Throw<ArgumentNullException>();
            action3.Should().Throw<ArgumentNullException>();
            action4.Should().Throw<ArgumentNullException>();
        }


        private class MyClass {
        }

        Error NewError(string message) => new Error(ErrorCode.INVALID_ARGUMENT, message);
    }
}
