using System;
using System.Runtime.Serialization;

namespace CSharpFunctionalExtensions.API {
    [Serializable]
    public sealed class ValidationError : IEquatable<ValidationError>, ISerializable {
        public ValidationError(string identifier, string errorMessage) {
            Identifier = identifier;
            ErrorMessage = errorMessage;
        }

        public ValidationError(string identifier, string errorMessage, ValidationSeverity severity) {
            Identifier = identifier;
            ErrorMessage = errorMessage;
            Severity = severity;
        }

        ValidationError(SerializationInfo info, StreamingContext context) {
            Identifier = info.GetString(nameof(Identifier));
            ErrorMessage = info.GetString(nameof(ErrorMessage));
            Severity = ValidationSeverity.FromValue(info.GetInt32(nameof(Severity)));
        }

        public string Identifier { get; }
        public string ErrorMessage { get; }
        public ValidationSeverity Severity { get; } = ValidationSeverity.Error;

        public override string ToString() => $"[{Identifier}, {ErrorMessage}, {Severity.Name}]";

        #region IEquatable

        public override bool Equals(object obj) => obj is ValidationError validationResult && Equals(validationResult);
        public bool Equals(ValidationError other) => other != null && EqualsCore(other);
        bool EqualsCore(ValidationError other) => 
            Identifier == other.Identifier &&
            ErrorMessage == other.ErrorMessage &&
            Severity.Value == other.Severity.Value;

        public static bool operator ==(ValidationError left, ValidationError right) => 
            !(left is null ^ right is null) && (left is null || left.EqualsCore(right));
        public static bool operator !=(ValidationError left, ValidationError right) => !(left == right);

        public override int GetHashCode() {
            unchecked {
                //391 = 17 * 23
                int hashCode = 391 + (Identifier?.GetHashCode() ?? 0);
                hashCode = (hashCode * 23) + (ErrorMessage?.GetHashCode() ?? 0);
                hashCode = (hashCode * 23) + Severity.Value.GetHashCode();

                return hashCode;
            }
        }

        #endregion

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Identifier), Identifier);
            info.AddValue(nameof(ErrorMessage), ErrorMessage);
            info.AddValue(nameof(Severity), Severity.Value);
        }

        #endregion
    }
}
