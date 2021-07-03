using System;

namespace CSharpFunctionalExtensions.API {
    public class ResultSuccessException : Exception {
        internal ResultSuccessException()
            : base(Result.Messages.ErrorIsInaccessibleForSuccess) {
        }
    }
}
