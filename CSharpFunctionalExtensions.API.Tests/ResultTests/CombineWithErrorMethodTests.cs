using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;


namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class CombineWithErrorMethodTests {
        [Fact]
        public void Combine_returns_Ok_if_no_failures_in_collection() {
            IEnumerable<Result<bool>> results = new Result<bool>[]
            {
                Result.Success<bool>(false),
                Result.Success<bool>(true),
                Result.Success<bool>(false)
            };

            Result<IEnumerable<bool>> result = results.Combine();

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures_in_Generic_results_collection() {
            IEnumerable<Result<int>> results = new Result<int>[]
            {
                Result.Success<int>(21),
                Result.Success<int>(34),
                Result.Success<int>(55)
            };

            Result<IEnumerable<int>> result = results.Combine();

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(new[] { 21, 34, 55 });
        }

        [Fact]
        public void Combine_works_with_collection_of_Generic_results_success() {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                Result.Success<string>("one"),
                Result.Success<string>("two"),
                Result.Success<string>("three")
            };

            Result<IEnumerable<string>> result = results.Combine();

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo("one", "two", "three");
        }

        [Fact]
        public void Combine_works_with_collection_of_results_and_compose_to_new_result_success() {
            IEnumerable<Result<int>> results = new Result<int>[]
            {
                Result.Success<int>(10),
                Result.Success<int>(20),
                Result.Success<int>(30),
            };

            Result<double> result = results.Combine(values => (double)values.Max() / 100);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(.3);
        }
    }
}
