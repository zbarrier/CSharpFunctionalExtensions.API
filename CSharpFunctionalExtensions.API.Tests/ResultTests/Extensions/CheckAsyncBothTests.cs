using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests.Extensions {
    public class CheckAsyncBothTests : CheckTestsBase {
        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void Check_T_AsyncBoth_func_result(bool resultSuccess, bool funcSuccess) {
            Result<T> result = Result.SuccessIf(resultSuccess, T.Value, ErrorMessage);

            var returned = result.AsTask().Check(_ => GetResult(funcSuccess).AsTask()).Result;

            actionExecuted.Should().Be(resultSuccess);
            returned.Should().Be(funcSuccess ? result : FailedResultT);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void Check_T_AsyncBoth_func_result_K(bool resultSuccess, bool funcSuccess) {
            Result<T> result = Result.SuccessIf(resultSuccess, T.Value, ErrorMessage);

            var returned = result.AsTask().Check(Func_Task_Result_K(funcSuccess)).Result;

            actionExecuted.Should().Be(resultSuccess);
            returned.Should().Be(funcSuccess ? result : FailedResultT);
        }
    }
}