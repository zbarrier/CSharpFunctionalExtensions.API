using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class EnsureMethodTests {
        [Fact]
        public void Ensure_source_result_is_failure_predicate_do_not_invoked_expect_is_result_failure() {
            Result sut = Result.Failure(NewError("some error"));

            Result result = sut.Ensure(() => true, NewError(string.Empty));

            result.Should().Be(sut);
        }

        [Fact]
        public void Ensure_source_result_is_success_predicate_is_failed_expected_result_failure() {
            Result sut = Result.Success();

            Result result = sut.Ensure(() => false, NewError("predicate failed"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate failed");
        }

        [Fact]
        public void Ensure_source_result_is_success_predicate_is_passed_expected_result_success() {
            Result sut = Result.Success();

            Result result = sut.Ensure(() => true, NewError(string.Empty));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_source_result_is_failure_async_predicate_do_not_invoked_expect_is_result_failure() {
            Result sut = Result.Failure(NewError("some error"));

            Result result = await sut.Ensure(() => Task.FromResult(true), NewError(string.Empty));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_source_result_is_success_async_predicate_is_failed_expected_result_failure() {
            Result sut = Result.Success();

            Result result = await sut.Ensure(() => Task.FromResult(false), NewError("predicate problems"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate problems");
        }

        [Fact]
        public async Task Ensure_source_result_is_success_async_predicate_is_passed_expected_result_success() {
            Result sut = Result.Success();

            Result result = await sut.Ensure(() => Task.FromResult(true), NewError(string.Empty));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_task_source_result_is_failure_predicate_do_not_invoked_expect_is_result_failure() {
            Task<Result> sut = Task.FromResult(Result.Failure(NewError("some error")));

            Result result = await sut.Ensure(() => true, NewError(string.Empty));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_task_source_result_is_success_predicate_is_failed_expected_result_failure() {
            Task<Result> sut = Task.FromResult(Result.Success());

            Result result = await sut.Ensure(() => false, NewError("predicate problems"));

            result.Should().NotBe(sut.Result);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate problems");
        }

        [Fact]
        public async Task Ensure_task_source_result_is_success_predicate_is_passed_expected_result_success() {
            Task<Result> sut = Task.FromResult(Result.Success());

            Result result = await sut.Ensure(() => true, NewError(string.Empty));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_task_source_result_is_failure_async_predicate_do_not_invoked_expect_is_result_failure() {
            Task<Result> sut = Task.FromResult(Result.Failure(NewError("some error")));

            Result result = await sut.Ensure(() => Task.FromResult(false), NewError(string.Empty));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_task_source_result_is_success_async_predicate_is_failed_expected_result_failure() {
            Task<Result> sut = Task.FromResult(Result.Success());

            Result result = await sut.Ensure(() => Task.FromResult(false), NewError("predicate problems"));

            result.Should().NotBe(sut.Result);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("predicate problems");
        }

        [Fact]
        public async Task Ensure_task_source_result_is_success_async_predicate_is_passed_expected_result_success() {
            Task<Result> sut = Task.FromResult(Result.Success());

            Result result = await sut.Ensure(() => Task.FromResult(true), NewError(string.Empty));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public void Ensure_generic_source_result_is_failure_predicate_do_not_invoked_expect_is_error_result_failure() {
            Result<TimeSpan> sut = Result.Failure<TimeSpan>(NewError("some error"));

            Result<TimeSpan> result = sut.Ensure(time => true, NewError("test error"));

            result.Should().Be(sut);
        }

        [Fact]
        public void Ensure_generic_source_result_is_success_predicate_is_failed_expected_error_result_failure() {
            Result<int> sut = Result.Success(10101);

            Result<int> result = sut.Ensure(i => false, NewError("test error"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("test error");
        }

        [Fact]
        public void Ensure_generic_source_result_is_success_predicate_is_passed_expected_error_result_success() {
            Result<decimal> sut = Result.Success(.03m);

            Result<decimal> result = sut.Ensure(d => true, NewError("test error"));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_generic_source_result_is_failure_async_predicate_do_not_invoked_expect_is_error_result_failure() {
            Result<DateTimeOffset> sut = Result.Failure<DateTimeOffset>(NewError("some result error"));

            Result<DateTimeOffset> result = await sut.Ensure(d => Task.FromResult(true), NewError("test ensure error"));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_generic_source_result_is_success_async_predicate_is_failed_expected_error_result_failure() {
            Result<int> sut = Result.Success(333);

            Result<int> result = await sut.Ensure(i => Task.FromResult(false), NewError("test ensure error"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("test ensure error");
        }

        [Fact]
        public async Task Ensure_generic_source_result_is_success_async_predicate_is_passed_expected_error_result_success() {
            Result<decimal> sut = Result.Success(.33m);

            Result<decimal> result = await sut.Ensure(d => Task.FromResult(true), NewError("test error"));

            result.Should().Be(sut);
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_failure_async_predicate_do_not_invoked_expect_is_error_result_failure() {
            Task<Result<TimeSpan>> sut = Task.FromResult(Result.Failure<TimeSpan>(NewError("some result error")));

            Result<TimeSpan> result = await sut.Ensure(t => Task.FromResult(true), NewError("test ensure error"));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_success_async_predicate_is_failed_expected_error_result_failure() {
            Task<Result<long>> sut = Task.FromResult(Result.Success<long>(333));

            Result<long> result = await sut.Ensure(l => Task.FromResult(false), NewError("test ensure error"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("test ensure error");
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_success_async_predicate_is_passed_expected_error_result_success() {
            Task<Result<double>> sut = Task.FromResult(Result.Success(.33));

            Result<double> result = await sut.Ensure(d => Task.FromResult(true), NewError("test error"));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_failure_predicate_do_not_invoked_expect_is_error_result_failure() {
            Task<Result<TimeSpan>> sut = Task.FromResult(Result.Failure<TimeSpan>(NewError("some result error")));

            Result<TimeSpan> result = await sut.Ensure(t => true, NewError("test ensure error"));

            result.Should().Be(sut.Result);
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_success_predicate_is_failed_expected_error_result_failure() {
            Task<Result<long>> sut = Task.FromResult(Result.Success<long>(333));

            Result<long> result = await sut.Ensure(l => false, NewError("test ensure error"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Message.Should().Be("test ensure error");
        }

        [Fact]
        public async Task Ensure_generic_task_source_result_is_success_predicate_is_passed_expected_error_result_success() {
            Task<Result<double>> sut = Task.FromResult(Result.Success(.33));

            Result<double> result = await sut.Ensure(d => true, NewError("test error"));

            result.Should().Be(sut.Result);
        }

        Error NewError(string message) => new Error(ErrorCode.INVALID_ARGUMENT, message);
    }
}