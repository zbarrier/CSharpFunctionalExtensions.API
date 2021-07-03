using System;
using System.Runtime.Serialization;

namespace CSharpFunctionalExtensions.API.Internal {
    internal static class ResultCommonLogic {
        internal static void GetObjectDataCommon(IResult result, SerializationInfo info) {
            info.AddValue("IsFailure", result.IsFailure);
            info.AddValue("IsSuccess", result.IsSuccess);
        }

        internal static void GetObjectData(Result result, SerializationInfo info) {
            GetObjectDataCommon(result, info);
            if (result.IsFailure) {
                info.AddValue("Error", result.Error);
            }
        }

        internal static void GetObjectData<T>(Result<T> result, SerializationInfo info, IValue<T> valueResult) {
            GetObjectDataCommon(result, info);
            if (result.IsFailure) {
                info.AddValue("Error", result.Error);
            }

            if (result.IsSuccess) {
                info.AddValue("Value", valueResult.Value);
            }
        }

        internal static bool ErrorStateGuard(bool isFailure, Error error) {
            if (isFailure) {
                if (error == null || string.IsNullOrWhiteSpace(error.Message))
                    throw new ArgumentNullException(nameof(error), Result.Messages.ErrorObjectIsNotProvidedForFailure);
            }
            else {
                if (error != default)
                    throw new ArgumentException(Result.Messages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }

            return isFailure;
        }

        internal static Error GetErrorWithSuccessGuard(bool isFailure, Error error) =>
            isFailure ? error : throw new ResultSuccessException();

        internal static SerializationValue Deserialize(SerializationInfo info) {
            bool isFailure = info.GetBoolean("IsFailure");
            Error error = isFailure ? (Error)info.GetValue("Error", typeof(Error)) : default;
            return new SerializationValue(isFailure, error);
        }
    }
}