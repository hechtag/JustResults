using Hechtag.JustResults.Errors;
using Hechtag.JustResults.Extensions;

namespace Hechtag.JustResults.Tests;

public sealed class OnSuccess
{
    public sealed class No_Content
    {
        [Fact]
        public void Create_Failure()
        {
            // arrange
            var successCount = 0;

            // act
            var failure = Result.Failure("message");
            failure.OnSuccess(() => successCount++);

            // assert
            using (new AssertionScope())
            {
                successCount.Should().Be(0);
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var successCount = 0;

            // act
            var success = Result.Success();
            success.OnSuccess(() => successCount++);

            // assert
            using (new AssertionScope())
            {
                successCount.Should().Be(1);
            }
        }
    }

    public sealed class With_Content
    {
        [Fact]
        public void Create_Failure()
        {
            // arrange
            var successCount = 0;
            string? resultValue = null;

            // act
            var failure = Result<string>.Failure("message");
            failure.OnSuccess(result =>
            {
                successCount++;
                resultValue = result;
            });

            // assert
            using (new AssertionScope())
            {
                successCount.Should().Be(0);
                resultValue.Should().BeNull();
            }
        }

        [Fact]
        public void Create_Success()
        {
            // arrange
            var successCount = 0;
            string? resultValue = null;

            // act
            var failure = Result<string>.Success("success");
            failure.OnSuccess(result =>
            {
                successCount++;
                resultValue = result;
            });

            // assert
            using (new AssertionScope())
            {
                successCount.Should().Be(1);
                resultValue.Should().Be("success");
            }
        }
    }
}