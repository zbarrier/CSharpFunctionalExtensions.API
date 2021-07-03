using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class ConvertFailureTests {
        static readonly Error _error = new Error(ErrorCode.INVALID_ARGUMENT, "Failed");

        [Fact]
        public void Can_not_convert_okResult_without_value_to_okResult_with_value() {
            var okResultWithoutValue = Result.Success();

            Action action = () => okResultWithoutValue.ConvertFailure<MyValueClass>();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_without_value_to_failedResult_with_value() {
            var failedResultWithoutValue = Result.Failure(_error);

            Result<MyValueClass> failedResultWithValue = failedResultWithoutValue.ConvertFailure<MyValueClass>();

            failedResultWithValue.IsFailure.Should().BeTrue();
            failedResultWithValue.Error.Message.Should().Be("Failed");
        }

        [Fact]
        public void Can_not_convert_okResult_with_value_to_okResult_without_value() {
            var okResultWithValue = Result.Success(new MyValueClass());

            Action action = () => okResultWithValue.ConvertFailure();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_with_value_to_failedResult_without_value() {
            var failedResultWithValue = Result.Failure<MyValueClass>(_error);

            Result failedResultWithoutValue = failedResultWithValue;

            failedResultWithoutValue.IsFailure.Should().BeTrue();
            failedResultWithoutValue.Error.Message.Should().Be("Failed");
        }

        [Fact]
        public void Can_not_convert_okResult_with_value_to_okResult_with_otherValue() {
            var okResultWithValue = Result.Success(new MyValueClass());

            Action action = () => okResultWithValue.ConvertFailure<MyValueClass2>();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_with_value_to_failedResult_with_other_value() {
            var failedResultWithValue = Result.Failure<MyValueClass>(_error);

            Result<MyValueClass2> failedResultWithOtherValue = failedResultWithValue.ConvertFailure<MyValueClass2>();

            failedResultWithOtherValue.IsFailure.Should().BeTrue();
            failedResultWithOtherValue.Error.Message.Should().Be("Failed");
        }
    }

    class MyValueClass {
        public int Prop { get; set; }
    }

    class MyValueClass2 {
        public int Prop { get; set; }
    }
}