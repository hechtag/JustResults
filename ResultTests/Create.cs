using Results;
using Results.Errors;

namespace ResultTests;

public sealed class Create
{
    public sealed class No_Content
    {
        public sealed class Create_Failure
        {
            [Fact]
            public void From_String()
            {
                // arrange
                var expected_failure_message = "expected_failure_message";

                // act
                var failure = Result.Failure(expected_failure_message);
                var result_message = failure.Match(() => "", err => err.Message);

                // assert
                using (new AssertionScope())
                {
                    failure.IsSuccess.Should().BeFalse();
                    result_message.Should().Be(expected_failure_message);
                }
            }

            [Fact]
            public void From_Error()
            {
                // arrange
                var error = TextError.Create("error");

                // act
                var failure = Result.Failure(error);
                var result_message = failure.Match(() => "", err => err.Message);

                // assert
                using (new AssertionScope())
                {
                    failure.IsSuccess.Should().BeFalse();
                    result_message.Should().Be(error.Message);
                }
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var expected_success_message = "expected_success_message";

            // act
            var failure = Result.Success();
            var result_message = failure.Match(() => expected_success_message, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeTrue();
                result_message.Should().Be(expected_success_message);
            }
        }
    }

    public sealed class With_Content
    {
        public sealed class Create_Failure
        {
            [Fact]
            public void From_String()
            {
                // arrange
                var expected_failure_message = "expected_failure_message";

                // act
                var failure = Result<string>.Failure(expected_failure_message);
                var result_message = failure.Match(() => "", err => err.Message);

                // assert
                using (new AssertionScope())
                {
                    failure.IsSuccess.Should().BeFalse();
                    result_message.Should().Be(expected_failure_message);
                }
            }

            [Fact]
            public void From_Error()
            {
                // arrange
                var error = TextError.Create("error");

                // act
                var failure = Result<string>.Failure(error);
                var result_message = failure.Match(() => "", err => err.Message);

                // assert
                using (new AssertionScope())
                {
                    failure.IsSuccess.Should().BeFalse();
                    result_message.Should().Be(error.Message);
                }
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var expected_success_message = "expected_success_message";

            // act
            var failure = Result<string>.Success(expected_success_message);
            var result_message = failure.Match(data => data, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeTrue();
                result_message.Should().Be(expected_success_message);
            }
        }
    }
}