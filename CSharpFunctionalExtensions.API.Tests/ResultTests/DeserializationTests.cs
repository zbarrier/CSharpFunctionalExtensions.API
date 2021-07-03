using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.API.Tests.ResultTests {
    public class DeserializationTests {
        private static readonly Error _error = new Error(ErrorCode.INVALID_ARGUMENT, "this failed");

        [Fact]
        public void Deserialize_sets_correct_statuses_on_success_result() {
            Result okResult = Result.Success();
            var serialized = Serialize(okResult);

            Result result = Deserialize<Result>(serialized);

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public void Deserialize_sets_correct_statuses_on_failure_result() {
            Result okResult = Result.Failure(_error);
            var serialized = Serialize(okResult);

            Result result = Deserialize<Result>(serialized);

            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_adds_message_in_context_on_failure_result() {
            Result failResult = Result.Failure(_error);
            var serialized = Serialize(failResult);

            Result result = Deserialize<Result>(serialized);

            result.Error.Should().Be(_error);
        }

        [Fact]
        public void Deserialize_of_generic_result_adds_object_in_context_when_success_result() {
            DeserializationTestObject language = new DeserializationTestObject { Number = 232, String = "C#" };
            Result<DeserializationTestObject> failResult = Result.Success(language);
            var serialized = Serialize(failResult);

            Result<DeserializationTestObject> result = Deserialize<Result<DeserializationTestObject>>(serialized);

            result.Value.Should().BeEquivalentTo(language);
        }

        private static Stream Serialize(object source) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }

        private static T Deserialize<T>(Stream stream) {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        [Serializable]
        private class DeserializationTestObject {
            public string String { get; set; }
            public int Number { get; set; }
        }
    }
}