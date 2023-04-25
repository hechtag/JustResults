using Hechtag.JustResults.Errors;
using Hechtag.JustResults.Extensions;

namespace Hechtag.JustResults.Tests;

public sealed class OnFailure
{
    public sealed class No_Content
    {
        [Fact]
        public void Create_Failure()
        {
            // arrange
            var failureCount = 0;
            IError? error = null;

            // act
            var failure = Result.Failure("error");
            failure.OnFailure(err =>
            {
                failureCount++;
                error = err;
            });

            // assert
            using (new AssertionScope())
            {
                failureCount.Should().Be(1);
                error.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var failureCount = 0;
            IError? error = null;

            // act
            var success = Result.Success();
            success.OnFailure(err =>
            {
                failureCount++;
                error = err;
            });

            // assert
            using (new AssertionScope())
            {
                failureCount.Should().Be(0);
                error.Should().BeNull();
            }
        }
    }

    public sealed class With_Content
    {
        [Fact]
        public void Create_Failure()
        {
            // arrange
            var failureCount = 0;
            IError? error = null;

            // act
            var failure = Result<string>.Failure("error");
            failure.OnFailure(err =>
            {
                failureCount++;
                error = err;
            });

            // assert
            using (new AssertionScope())
            {
                failureCount.Should().Be(1);
                error.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var failureCount = 0;
            IError? error = null;

            // act
            var failure = Result<string>.Success("success");
            failure.OnFailure(err =>
            {
                failureCount++;
                error = err;
            });

            // assert
            using (new AssertionScope())
            {
                failureCount.Should().Be(0);
                error.Should().BeNull();
            }
        }
    }
}