using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class ResultTests {
        [Fact]
        public void Ok_argument_is_null_Success_result_expected() {
            Result result = Result.Success<string>(null);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Create_value_is_null_Success_result_expected() {
            Result result = Result.SuccessIf<string>(true, null, null);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Create_argument_is_true_Success_result_expected() {
            Result result = Result.SuccessIf(true, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Create_argument_is_false_Failure_result_expected() {
            Result result = Result.SuccessIf(false, new Error(ErrorCode.INVALID_ARGUMENT, "simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("simple result error");
        }

        [Fact]
        public void Create_predicate_is_true_Success_result_expected() {
            Result result = Result.SuccessIf(() => true, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Create_predicate_is_false_Failure_result_expected() {
            Result result = Result.SuccessIf(() => false, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public async Task Create_async_predicate_is_true_Success_result_expected() {
            Result result = await Result.SuccessIf(() => Task.FromResult(true), new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Create_async_predicate_is_false_Failure_result_expected() {
            Result result = await Result.SuccessIf(() => Task.FromResult(false), new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public void CreateFailure_value_is_null_Success_result_expected() {
            Result result = Result.FailureIf<string>(false, null, null);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_argument_is_false_Success_result_expected() {
            Result result = Result.FailureIf(false, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_argument_is_true_Failure_result_expected() {
            Result result = Result.FailureIf(true, new Error(ErrorCode.INVALID_ARGUMENT, "simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("simple result error");
        }

        [Fact]
        public void CreateFailure_predicate_is_false_Success_result_expected() {
            Result result = Result.FailureIf(() => false, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_predicate_is_true_Failure_result_expected() {
            Result result = Result.FailureIf(() => true, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public async Task CreateFailure_async_predicate_is_false_Success_result_expected() {
            Result result = await Result.FailureIf(() => Task.FromResult(false), new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task CreateFailure_async_predicate_is_true_Failure_result_expected() {
            Result result = await Result.FailureIf(() => Task.FromResult(true), new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public void Create_generic_argument_is_true_Success_result_expected() {
            byte val = 7;
            Result<byte> result = Result.SuccessIf(true, val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void Create_generic_argument_is_false_Failure_result_expected() {
            double val = .56;
            Result<double> result = Result.SuccessIf(false, val, new Error(ErrorCode.INVALID_ARGUMENT, "simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("simple result error");
        }

        [Fact]
        public void Create_generic_predicate_is_true_Success_result_expected() {
            DateTime val = new DateTime(2000, 1, 1);

            Result<DateTime> result = Result.SuccessIf(() => true, val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void Create_generic_predicate_is_false_Failure_result_expected() {
            string val = "string value";

            Result<string> result = Result.SuccessIf(() => false, val, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public async Task Create_generic_async_predicate_is_true_Success_result_expected() {
            int val = 42;

            Result<int> result = await Result.SuccessIf(() => Task.FromResult(true), val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public async Task Create_generic_async_predicate_is_false_Failure_result_expected() {
            bool val = true;

            Result<bool> result = await Result.SuccessIf(() => Task.FromResult(false), val, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public void CreateFailure_generic_argument_is_false_Success_result_expected() {
            byte val = 7;
            Result<byte> result = Result.FailureIf(false, val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_argument_is_true_Failure_result_expected() {
            double val = .56;
            Result<double> result = Result.FailureIf(true, val, new Error(ErrorCode.INVALID_ARGUMENT, "simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("simple result error");
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_false_Success_result_expected() {
            DateTime val = new DateTime(2000, 1, 1);

            Result<DateTime> result = Result.FailureIf(() => false, val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_true_Failure_result_expected() {
            string val = "string value";

            Result<string> result = Result.FailureIf(() => true, val, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public async Task CreateFailure_generic_async_predicate_is_false_Success_result_expected() {
            int val = 42;

            Result<int> result = await Result.FailureIf(() => Task.FromResult(false), val, new Error(ErrorCode.INVALID_ARGUMENT, string.Empty));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public async Task CreateFailure_generic_async_predicate_is_true_Failure_result_expected() {
            bool val = true;

            Result<bool> result = await Result.FailureIf(() => Task.FromResult(true), val, new Error(ErrorCode.INVALID_ARGUMENT, "predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate result error");
        }

        [Fact]
        public void Can_work_with_nullable_sructs() {
            Result<DateTime?> result = Result.Success((DateTime?)null);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(null);
        }

        [Fact]
        public void Can_work_with_maybe_of_struct() {
            Maybe<DateTime> maybe = Maybe<DateTime>.None;

            Result<Maybe<DateTime>> result = Result.Success(maybe);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Maybe<DateTime>.None);
        }

        [Fact]
        public void Can_work_with_maybe_of_ref_type() {
            Maybe<string> maybe = Maybe<string>.None;

            Result<Maybe<string>> result = Result.Success(maybe);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Maybe<string>.None);
        }

        [Fact]
        public void Try_execute_function_success_without_error_handler_function_result_expected() {
            Func<int> func = () => 5;

            var result = Result.Try(func);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(5);
        }

        [Fact]
        public void Try_execute_function_failed_without_error_handler_failed_result_expected() {
            Func<int> func = () => throw new Exception("func error");

            var result = Result.Try(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("func error");
        }

        [Fact]
        public void Try_execute_function_failed_with_error_handler_failed_result_expected() {
            Func<int> func = () => throw new Exception("func error");
            Func<Exception, Error> handler = exc => new Error(ErrorCode.INVALID_ARGUMENT, "execute error");

            var result = Result.Try(func, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute error");
        }

        [Fact]
        public void Try_execute_action_success_without_error_handler_function_result_expected() {
            Action action = () => { };

            var result = Result.Try(action);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Try_execute_action_failed_without_error_handler_failed_result_expected() {
            Action action = () => throw new Exception("func error");

            var result = Result.Try(action);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("func error");
        }

        [Fact]
        public void Try_execute_action_failed_with_error_handler_failed_result_expected() {
            Action action = () => throw new Exception("func error");
            Func<Exception, Error> handler = exc => new Error(ErrorCode.INVALID_ARGUMENT, "execute error");

            var result = Result.Try(action, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute error");
        }

        [Fact]
        public async Task Try_execute_async_action_success_without_error_handler_function_result_expected() {
            Func<Task> action = () => Task.CompletedTask;

            var result = await Result.Try(action);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Try_execute_async_action_failed_without_error_handler_failed_result_expected() {
            Func<Task> action = () => Task.FromException(new Exception("func error"));

            var result = await Result.Try(action);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("func error");
        }

        [Fact]
        public async Task Try_execute_async_action_failed_with_error_handler_failed_result_expected() {
            Func<Task> action = () => Task.FromException(new Exception("func error"));
            Func<Exception, Error> handler = exc => new Error(ErrorCode.INVALID_ARGUMENT, "execute error");

            var result = await Result.Try(action, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute error");
        }

        [Fact]
        public void Try_with_error_execute_function_success_without_error_success_result_expected() {
            Func<string> func = () => "execution result";
            var error = new Error(ErrorCode.INVALID_ARGUMENT, "");

            var result = Result.Try(func, exc => error);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("execution result");
        }

        [Fact]
        public void Try_with_error_execute_function_failed_with_error_handler_failed_result_expected() {
            Func<int> func = () => throw new Exception("func error");
            var error = new Error(ErrorCode.INVALID_ARGUMENT, "a");

            var result = Result.Try(func, exc => error);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be(error.Message);
        }

        [Fact]
        public async Task Try_async_execute_function_success_without_error_handler_function_result_expected() {
            Func<Task<int>> func = () => Task.FromResult(5);

            var result = await Result.Try(func);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(5);
        }

        [Fact]
        public async Task Try_async_execute_function_failed_without_error_handler_failed_result_expected() {
            Func<Task<int>> func = () => Task.FromException<int>(new Exception("func error"));

            var result = await Result.Try(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("func error");
        }

        [Fact]
        public async Task Try_async_execute_function_failed_with_error_handler_failed_result_expected() {
            Func<Task<int>> func = () => Task.FromException<int>(new Exception("func error"));
            Func<Exception, Error> handler = exc => new Error(ErrorCode.INVALID_ARGUMENT, "execute error");

            var result = await Result.Try(func, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("execute error");
        }

        [Fact]
        public async Task Try_async_with_error_execute_function_success_without_error_success_result_expected() {
            Func<Task<string>> func = () => Task.FromResult("execution result");

            var result = await Result.Try(func, exc => new Error(ErrorCode.INVALID_ARGUMENT, ""));

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("execution result");
        }

        [Fact]
        public async Task Try_async_with_error_execute_function_failed_with_error_handler_failed_result_expected() {
            Func<Task<DateTime>> func = () => Task.FromException<DateTime>(new Exception("func error"));
            var error = new Error(ErrorCode.INVALID_ARGUMENT, "a");

            var result = await Result.Try(func, exc => error);

            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be(error.Message);
        }

        [Fact]
        public async Task When_Asynchronous_Continuation_Passed_To_On_Success_Try_Then_Needs_To_Be_Handled_As_Awaitable() {
            var content = "my key";
            var result = Result.Success<string>(content);

            await result
                .OnSuccessTry(async (key) => {
                    await Task.Delay(10);
                    var task = Task.FromResult(key);
                    var k = await task;
                    k.Should().Be(content);
                });
        }
    }
}