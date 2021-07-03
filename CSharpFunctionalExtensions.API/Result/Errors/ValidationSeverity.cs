using System;
using Ardalis.SmartEnum;

namespace CSharpFunctionalExtensions.API {
    public sealed class ValidationSeverity : SmartEnum<ValidationSeverity>, IEquatable<ValidationSeverity> {
        public static readonly ValidationSeverity Error = new ValidationSeverity(nameof(Error), 0);
        public static readonly ValidationSeverity Warning = new ValidationSeverity(nameof(Warning), 1);
        public static readonly ValidationSeverity Info = new ValidationSeverity(nameof(Info), 2);

        ValidationSeverity(string name, int value) : base(name, value) {

        }

        #region IEquatable

        public override bool Equals(object obj) => obj is ValidationSeverity validationSeverity && Equals(validationSeverity);
        public bool Equals(ValidationSeverity other) => other != null && EqualsCore(other);
        bool EqualsCore(ValidationSeverity other) => Value == other.Value;

        public static bool operator ==(ValidationSeverity left, ValidationSeverity right) 
            => !(left is null ^ right is null) && (left is null || left.EqualsCore(right));
        public static bool operator !=(ValidationSeverity left, ValidationSeverity right) => !(left == right);

        public override int GetHashCode() => Value.GetHashCode();

        #endregion
    }
}
