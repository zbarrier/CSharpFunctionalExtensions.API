using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.MaybeTests {
    public class ImplicitConversionTests {
        [Fact]
        public void Implicit_conversion_of_reference_type() {
            StringVO stringVo = default;

            // ReSharper disable once ExpressionIsAlwaysNull
            string stringPrimitive = stringVo;

            stringPrimitive.Should().BeNull();
        }

        [Fact]
        public void Implicit_conversion_of_value_type() {
            IntVO intVo = default;

            // ReSharper disable once ExpressionIsAlwaysNull
            int intPrimitive = intVo;

            intPrimitive.Should().Be(0);
        }

        public record StringVO(string Value) {
            public static implicit operator string(StringVO self) => self?.Value ?? default;
        }

        public record IntVO(int Value) {
            public static implicit operator int(IntVO self) => self?.Value ?? 0;
        }
    }
}
