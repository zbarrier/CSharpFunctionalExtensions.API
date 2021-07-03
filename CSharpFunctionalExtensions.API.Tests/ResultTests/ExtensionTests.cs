using System;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class ExtensionTests {
        private static readonly Error _error = new Error(ErrorCode.INVALID_ARGUMENT, "this failed");

        [Fact]
        public void Should_execute_action_on_failure() {
            bool myBool = false;

            Result myResult = Result.Failure(_error);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_execute_action_on_generic_failure() {
            bool myBool = false;

            Result<MyClass> myResult = Result.Failure<MyClass>(_error);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_exexcute_action_with_result_on_generic_failure() {
            Error myError = NewError(string.Empty);

            Result<MyClass> myResult = Result.Failure<MyClass>(_error);
            myResult.OnFailure(error => myError = error);

            myError.Should().Be(_error);
        }

        [Fact]
        public void Should_execute_compensate_func_on_failure_returns_Ok() {
            var myResult = Result.Failure(_error);
            var newResult = myResult.OnFailureCompensate(() => Result.Success());

            newResult.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Should_execute_compensate_func_on_generic_failure_returns_Ok() {
            var expectedValue = new MyClass();

            var myResult = Result.Failure<MyClass>(_error);
            var newResult = myResult.OnFailureCompensate(() => Result.Success(expectedValue));

            newResult.IsSuccess.Should().BeTrue();
            newResult.Value.Should().Be(expectedValue);
        }

        [Fact]
        public void Should_execute_compensate_func_with_result_on_generic_failure_returns_Ok() {
            var expectedValue = new MyClass();

            var myResult = Result.Failure<MyClass>(_error);
            var newResult = myResult.OnFailureCompensate(error => Result.Success(expectedValue));

            newResult.IsSuccess.Should().BeTrue();
            newResult.Value.Should().Be(expectedValue);
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_action_original_failed_result_expected() {
            var originalResult = Result.Failure(NewError("error"));

            var result = originalResult.OnSuccessTry(() => { });

            result.IsFailure.Should().BeTrue();
            result.Should().Be(originalResult);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_success_result_expected() {
            var originalResult = Result.Success();
            bool isExecuted = false;

            var result = originalResult.OnSuccessTry(() => {
                isExecuted = true;
            });

            result.IsSuccess.Should().BeTrue();

            isExecuted.Should().BeTrue();
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_throw_exception_failed_result_expected() {
            var originalResult = Result.Success();

            var result = originalResult.OnSuccessTry((Action)(() => throw new Exception("execute action exception.")));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute action exception.");
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_function_new_failed_result_expected() {
            var originalResult = Result.Failure(NewError("original result error message"));

            Result<int> result = originalResult.OnSuccessTry(() => 3);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("original result error message");
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_success_result_expected() {
            var originalResult = Result.Success();

            Result<int> result = originalResult.OnSuccessTry(() => 7);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(7);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_throw_exception_failed_result_expected() {
            var originalResult = Result.Success();
            Func<DateTime> func = () => throw new Exception("execute action exception.");

            Result<DateTime> result = originalResult.OnSuccessTry(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute action exception.");
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_function_with_argument_new_failed_result_expected() {
            var originalResult = Result.Failure<DateTime>(NewError("original result error message"));

            Result<int> result = originalResult.OnSuccessTry(date => date.Day);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("original result error message");
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_with_argument_success_result_expected() {
            var originalResult = Result.Success<byte>(2);

            Result<int> result = originalResult.OnSuccessTry(val => val * val);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(4);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_with_argument_throw_exception_failed_result_expected() {
            var originalResult = Result.Success(2);
            Func<int, DateTime> func = val => throw new Exception("execute action exception");

            Result<DateTime> result = originalResult.OnSuccessTry(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute action exception");
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_action_with_argument_new_failed_result_expected() {
            var originalResult = Result.Failure<DateTime>(NewError("original result error message"));

            Result result = originalResult.OnSuccessTry(date => { });

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("original result error message");
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_with_argument_success_result_expected() {
            var originalResult = Result.Success<byte>(2);
            bool isExecuted = false;

            Result result = originalResult.OnSuccessTry(val => { isExecuted = true; });

            result.IsSuccess.Should().BeTrue();

            isExecuted.Should().BeTrue();
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_with_argument_throw_exception_failed_result_expected() {
            var originalResult = Result.Success(2);
            Action<int> action = val => throw new Exception("execute action exception");

            Result result = originalResult.OnSuccessTry(action);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute action exception");
        }


        [Fact]
        public void Match_for_Result_of_int_follows_Ok_branch_where_there_is_a_value() {
            var result = Result.Success(20);

            result.Match(
                onSuccess: (value) => value.Should().Be(20),
                onFailure: (_) => throw new FieldAccessException("Accessed Failure path while result is Ok")
            );
        }

        [Fact]
        public void Match_for_Result_of_int_follows_Failure_branch_where_is_no_value() {
            var result = Result.Failure<int>(NewError("error"));

            result.Match(
                onSuccess: (_) => throw new FieldAccessException("Accessed Ok path while result is Failure"),
                onFailure: (error) => error.Message.Should().Be("error")
            );
        }

        [Fact]
        public void Match_for_empty_Result_follows_Ok_branch_where_there_is_a_value() {
            var result = Result.Success();

            result.Match(
                onSuccess: () => Assert.True(true),
                onFailure: (_) => throw new FieldAccessException("Accessed Failure path while result is Ok")
            );
        }

        [Fact]
        public void Match_for_empty_Result_follows_Failure_branch_where_is_no_value() {
            var result = Result.Failure(NewError("error"));

            result.Match(
                onSuccess: () => throw new FieldAccessException("Accessed Ok path while result is Failure"),
                onFailure: (error) => error.Message.Should().Be("error")
            );
        }

        private class MyClass {
            public string Property { get; set; }
        }

        Error NewError(string message) => new Error(ErrorCode.INVALID_ARGUMENT, message);
    }
}
