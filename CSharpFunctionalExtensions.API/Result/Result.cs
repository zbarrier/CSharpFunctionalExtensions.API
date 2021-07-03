using System;
using System.Runtime.Serialization;
using CSharpFunctionalExtensions.API.Internal;

namespace CSharpFunctionalExtensions.API {
    [Serializable]
    public partial struct Result : IResult, ISerializable
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        private readonly Error _error;
        public Error Error => ResultCommonLogic.GetErrorWithSuccessGuard(IsFailure, _error);

        private Result(bool isFailure, Error error)
        {
            IsFailure = ResultCommonLogic.ErrorStateGuard(isFailure, error);
            _error = error;
        }

        private Result(SerializationInfo info, StreamingContext context)
        {
            var values = ResultCommonLogic.Deserialize(info);
            IsFailure = values.IsFailure;
            _error = values.Error;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) =>
            ResultCommonLogic.GetObjectData(this, info);
    }
}
