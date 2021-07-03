using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CSharpFunctionalExtensions.API {
    [Serializable]
    public sealed class Error : ISerializable, IEquatable<Error> {
        public Error(ErrorCode code, string message) {
            Code = code;
            Message = message;
        }
        public Error(ErrorCode code, IEnumerable<ValidationError> validationErrors) {
            Code = code;
            ValidationErrors = validationErrors;
        }
        Error(SerializationInfo info, StreamingContext context) {
            Code = ErrorCode.FromValue(info.GetUInt16(nameof(Code)));
            Message = info.GetString(nameof(Message));
            ValidationErrors = (IEnumerable<ValidationError>)info.GetValue(nameof(ValidationErrors), typeof(IEnumerable<ValidationError>));
        }

        public ErrorCode Code { get; }
        public string? Message { get; }

        public bool IsValidationError => ValidationErrors.Any();
        public IEnumerable<ValidationError> ValidationErrors { get; } = new List<ValidationError>();

        public override string ToString() => 
            IsValidationError 
                ? $"{Code.Name}, [{string.Join(", ", ValidationErrors)}]" 
                : $"{Code.Name}, {Message}";

        #region IEquatable

        public override bool Equals(object obj) => obj is Error error && EqualsCore(error);
        public bool Equals(Error other) => other != null && EqualsCore(other);
        bool EqualsCore(Error other) => 
            Code.Value == other.Code.Value &&
            Message == other.Message &&
            ValidationErrors.SequenceEqual(other.ValidationErrors);

        public static bool operator ==(Error left, Error right) => 
            !(left is null ^ right is null) && (left is null || left.EqualsCore(right));
        public static bool operator !=(Error left, Error right) => !(left == right);

        public override int GetHashCode() {
            unchecked {
                int hashCode = 17;

                hashCode = (hashCode * 23) + Code.Value.GetHashCode();
                hashCode = (hashCode * 23) + (Message?.GetHashCode() ?? 0);
                hashCode = ValidationErrors.Aggregate(hashCode, (current, validationError) => (current * 23) + (validationError?.GetHashCode() ?? 0));

                return hashCode;
            }
        }

        #endregion

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Code), Code.Value);
            info.AddValue(nameof(Message), Message);
            info.AddValue(nameof(ValidationErrors), ValidationErrors, typeof(IEnumerable<ValidationError>));
        }

        #endregion
    }
}
