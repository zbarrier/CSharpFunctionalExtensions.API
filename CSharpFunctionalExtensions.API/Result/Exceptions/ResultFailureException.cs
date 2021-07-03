﻿using System;

namespace CSharpFunctionalExtensions.API {
    public class ResultFailureException : Exception {
        public Error Error { get; }

        internal ResultFailureException(Error error)
            : base(Result.Messages.ValueIsInaccessibleForFailure(error)) {
            Error = error;
        }
    }
}
