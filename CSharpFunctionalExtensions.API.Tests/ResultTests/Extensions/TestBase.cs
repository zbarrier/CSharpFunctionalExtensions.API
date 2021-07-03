namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public abstract class TestBase {
        protected readonly static Error ErrorMessage = new Error(ErrorCode.INVALID_ARGUMENT, "Error Message");

        protected class T {
            public static readonly T Value = new T();
        }

        protected class K {
            public static readonly K Value = new K();
        }

        protected class E {
            public static readonly E Value = new E();
        }

        protected class E2 {
            public static readonly E2 Value = new E2();
        }

        protected Error NewError(string message) => new Error(ErrorCode.INVALID_ARGUMENT, message);
    }
}
