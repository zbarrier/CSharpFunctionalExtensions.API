using System.Collections.Generic;
using System.Linq;

namespace CSharpFunctionalExtensions.API {
    public partial struct Result {
        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.</summary>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <param name="errorMessagesSeparator">
        ///     A string that is used to separate any concatenated error messages. If omitted, the default <see cref="Result.ErrorMessagesSeparator" /> is used.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine(IEnumerable<Result> results, string errorMessagesSeparator = null) {
            List<Result> failedResults = results.Where(x => x.IsFailure).ToList();

            if (failedResults.Count == 0)
                return Success();

            ErrorCode code = failedResults.Select(x => x.Error.Code).Combine();
            string message = string.Join(errorMessagesSeparator ?? ErrorMessagesSeparator, AggregateMessages(failedResults.Select(x => x.Error.Message)));

            return Failure(new Error(code, message));
        }

        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.</summary>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <param name="errorMessagesSeparator">
        ///     A string that is used to separate any concatenated error messages. If omitted, the default <see cref="Result.ErrorMessagesSeparator" /> is used.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine<T>(IEnumerable<Result<T>> results, string errorMessagesSeparator = null) {
            IEnumerable<Result> untyped = results.Select(result => (Result)result);
            return Combine(untyped, errorMessagesSeparator);
        }

        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.
        ///     Error messages are concatenated with the default <see cref="Result.ErrorMessagesSeparator" /> between each message.</summary>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine(params Result[] results)
            => Combine(results, ErrorMessagesSeparator);

        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.
        ///     Error messages are concatenated with the default <see cref="Result.ErrorMessagesSeparator" /> between each message.</summary>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine<T>(params Result<T>[] results)
            => Combine(results, ErrorMessagesSeparator);

        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.</summary>
        /// <param name="errorMessagesSeparator">
        ///     A string that is used to separate any concatenated error messages. If omitted, the default <see cref="Result.ErrorMessagesSeparator" /> is used.</param>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine(string errorMessagesSeparator, params Result[] results)
            => Combine(results, errorMessagesSeparator);

        /// <summary>
        ///     Combines several results (and any error messages) into a single result.
        ///     The returned result will be a failure if any of the input <paramref name="results"/> are failures.</summary>
        /// <param name="errorMessagesSeparator">
        ///     A string that is used to separate any concatenated error messages. If omitted, the default <see cref="Result.ErrorMessagesSeparator" /> is used.</param>
        /// <param name="results">
        ///     The Results to be combined.</param>
        /// <returns>
        ///     A Result that is a success when all the input <paramref name="results"/> are also successes.</returns>
        public static Result Combine<T>(string errorMessagesSeparator, params Result<T>[] results)
            => Combine(results, errorMessagesSeparator);

        private static IEnumerable<string> AggregateMessages(IEnumerable<string> messages) {
            var dict = new Dictionary<string, int>();
            foreach (var message in messages) {
                if (!dict.ContainsKey(message))
                    dict.Add(message, 0);

                dict[message]++;
            }

            return dict.Select(x => x.Value == 1 ? x.Key : $"{x.Key} ({x.Value}×)");
        }
    }
}
