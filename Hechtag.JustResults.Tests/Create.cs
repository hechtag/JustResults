using Hechtag.JustResults.Errors;
using Hechtag.JustResults.Extensions;

namespace Hechtag.JustResults.Tests;

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

            [Fact]
            public void ToFailure()
            {
                // arrange
                var input = "input";

                // act
                var result = input.ToFailure<string>();

                // assert
                using (new AssertionScope())
                {
                    result.IsSuccess.Should().BeFalse();
                    result.GetError().Message.Should().Be(input);
                }
            }

            [Fact]
            public async Task ToFailure_async()
            {
                // arrange
                var input = Task.FromResult("input");

                // act
                var result = await input.ToFailure<string>();

                // assert
                using (new AssertionScope())
                {
                    result.IsSuccess.Should().BeFalse();
                    result.GetError().Message.Should().Be(await input);
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

        [Fact]
        public async Task Create_Success_async()
        {
            // arrange
            var input = "input".ToTask();

            // act
            var failure = await Result<string>.Success(input);
            var result_message = failure.Match(data => data, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeTrue();
                result_message.Should().Be("input");
            }
        }

        [Fact]
        public void ToSuccess()
        {
            // arrange
            var input = "input";

            // act
            var result = input.ToSuccess();

            // assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.GetValue().Should().Be(input);
            }
        }

        [Fact]
        public async Task ToSuccess_async()
        {
            // arrange
            var input = Task.FromResult("input");

            // act
            var result = await input.ToSuccess();


            // assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.GetValue().Should().Be(await input);
            }
        }
    }
}