﻿using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class TapIfAsyncBoth : TapIfTestsBase {
        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TapIf_AsyncBoth_executes_action_conditionally_and_returns_self(bool isSuccess, bool condition) {
            Result result = Result.SuccessIf(isSuccess, ErrorMessage);

            var returned = result.AsTask().TapIf(condition, Task_Action).Result;

            actionExecuted.Should().Be(isSuccess && condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TapIf_T_AsyncBoth_executes_action_conditionally_and_returns_self(bool isSuccess, bool condition) {
            Result<T> result = Result.SuccessIf(isSuccess, T.Value, ErrorMessage);

            var returned = result.AsTask().TapIf(condition, Task_Action).Result;

            actionExecuted.Should().Be(isSuccess && condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TapIf_T_AsyncBoth_executes_action_T_conditionally_and_returns_self(bool isSuccess, bool condition) {
            Result<T> result = Result.SuccessIf(isSuccess, T.Value, ErrorMessage);

            var returned = result.AsTask().TapIf(condition, Task_Action_T).Result;

            actionExecuted.Should().Be(isSuccess && condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TapIf_T_AsyncBoth_executes_action_per_predicate_and_returns_self(bool isSuccess, bool condition) {
            Result<bool> result = Result.SuccessIf(isSuccess, condition, ErrorMessage);

            var returned = result.AsTask().TapIf(Predicate, Task_Action).Result;

            predicateExecuted.Should().Be(isSuccess);
            actionExecuted.Should().Be(isSuccess && condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void TapIf_T_AsyncBoth_executes_action_T_per_predicate_and_returns_self(bool isSuccess, bool condition) {
            Result<bool> result = Result.SuccessIf(isSuccess, condition, ErrorMessage);

            var returned = result.AsTask().TapIf(Predicate, Task_Action_T).Result;

            predicateExecuted.Should().Be(isSuccess);
            actionExecuted.Should().Be(isSuccess && condition);
            result.Should().Be(returned);
        }
    }
}
