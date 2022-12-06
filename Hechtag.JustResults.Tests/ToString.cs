namespace Hechtag.JustResults.Tests;

public sealed class ToString
{
    public sealed class With_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var success = Result<string>.Success("success");

            // act
            var res = success.ToString();

            // assert
            res.Should().Be("Result: Success (success)");
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var failure = Result<string>.Failure("failure");

            // act
            var res = failure.ToString();

            // assert
            res.Should().Be("Result: Failure (TextError: failure)");
        }
    }

    public sealed class No_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var success = Result.Success();

            // act
            var res = success.ToString();

            // assert
            res.Should().Be("Result: Success");
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var failure = Result.Failure("failure");

            // act
            var res = failure.ToString();

            // assert
            res.Should().Be("Result: Failure (TextError: failure)");
        }
    }
}

