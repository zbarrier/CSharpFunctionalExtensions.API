using System;
using System.Collections.Generic;
using Ardalis.SmartEnum;

namespace CSharpFunctionalExtensions.API {
    public sealed class ErrorCode : SmartEnum<ErrorCode, ushort>, IEquatable<ErrorCode> {
        public static readonly ErrorCode CANCELLED =           new ErrorCode(nameof(CANCELLED),            1, httpCode: 499, grpcCode:  1, priority: 10);
        public static readonly ErrorCode UNKNOWN =             new ErrorCode(nameof(UNKNOWN),              2, httpCode: 500, grpcCode:  2, priority:  3);
        public static readonly ErrorCode INVALID_ARGUMENT =    new ErrorCode(nameof(INVALID_ARGUMENT),     3, httpCode: 400, grpcCode:  3, priority: 12);
        public static readonly ErrorCode DEADLINE_EXCEEDED =   new ErrorCode(nameof(DEADLINE_EXCEEDED),    4, httpCode: 504, grpcCode:  4, priority:  6);
        public static readonly ErrorCode NOT_FOUND =           new ErrorCode(nameof(NOT_FOUND),            5, httpCode: 404, grpcCode:  5, priority: 15);
        public static readonly ErrorCode ALREADY_EXISTS =      new ErrorCode(nameof(ALREADY_EXISTS),       6, httpCode: 409, grpcCode:  6, priority: 16);
        public static readonly ErrorCode PERMISSION_DENIED =   new ErrorCode(nameof(PERMISSION_DENIED),    7, httpCode: 403, grpcCode:  7, priority:  8);
        public static readonly ErrorCode RESOURCE_EXHAUSTED =  new ErrorCode(nameof(RESOURCE_EXHAUSTED),   8, httpCode: 429, grpcCode:  8, priority:  9);
        public static readonly ErrorCode FAILED_PRECONDITION = new ErrorCode(nameof(FAILED_PRECONDITION),  9, httpCode: 400, grpcCode:  9, priority: 13);
        public static readonly ErrorCode ABORTED =             new ErrorCode(nameof(ABORTED),             10, httpCode: 409, grpcCode: 10, priority: 11);
        public static readonly ErrorCode OUT_OF_RANGE =        new ErrorCode(nameof(OUT_OF_RANGE),        11, httpCode: 400, grpcCode: 11, priority: 14);
        public static readonly ErrorCode UNIMPLEMENTED =       new ErrorCode(nameof(UNIMPLEMENTED),       12, httpCode: 501, grpcCode: 12, priority:  4);
        public static readonly ErrorCode INTERNAL =            new ErrorCode(nameof(INTERNAL),            13, httpCode: 500, grpcCode: 13, priority:  2);
        public static readonly ErrorCode UNAVAILABLE =         new ErrorCode(nameof(UNAVAILABLE),         14, httpCode: 503, grpcCode: 14, priority:  5);
        public static readonly ErrorCode DATA_LOSS =           new ErrorCode(nameof(DATA_LOSS),           15, httpCode: 500, grpcCode: 15, priority:  1);
        public static readonly ErrorCode UNAUTHENTICATED =     new ErrorCode(nameof(UNAUTHENTICATED),     16, httpCode: 401, grpcCode: 16, priority:  7);

        ErrorCode(string name, ushort value, ushort httpCode, byte grpcCode, ushort priority) : base(name, value) {
            HttpCode = httpCode;
            GrpcCode = grpcCode;
            Priority = priority;
        }

        public ushort HttpCode { get; }
        public byte GrpcCode { get; }
        internal ushort Priority { get; }

        #region IEquatable

        public override bool Equals(object obj) => obj is ErrorCode errorCode && Equals(errorCode);
        public bool Equals(ErrorCode other) => other != null && EqualsCore(other);
        bool EqualsCore(ErrorCode other) => Value == other.Value;

        public static bool operator ==(ErrorCode left, ErrorCode right) => 
            !(left is null ^ right is null) && (left is null || left.EqualsCore(right));
        public static bool operator !=(ErrorCode left, ErrorCode right) => !(left == right);

        public override int GetHashCode() => Value.GetHashCode();

        #endregion
    }

    internal class ErrorCodeEqualityComparer : IEqualityComparer<ErrorCode> {
        public bool Equals(ErrorCode left, ErrorCode right) => left.Value == right.Value;

        public int GetHashCode(ErrorCode errorCode) => errorCode.GetHashCode();
    }
}
