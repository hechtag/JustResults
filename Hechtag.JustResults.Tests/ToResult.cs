namespace Hechtag.JustResults.Tests;

public sealed class ToResult
{
    public sealed class Sync
    {
        [Fact]
        public void Success()
        {
            // arrange
            var input = "input";
            var success = Result<string>.Success(input);

            // act
            var result = success.ToResult();

            // assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var error = "error";
            var failure = Result<string>.Failure(error);

            // act
            var result = failure.ToResult();

            // assert
            var res = result.GetError();
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeFalse();
                res.Message.Should().Be(error);
            }
        }
    }
    public sealed class Async
    {
        [Fact]
        public async Task Success()
        {
            // arrange
            var input = "input";
            var success = Result<string>.Success(input).ToTask();

            // act
            var result = await success.ToResultAsync();

            // assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Failure()
        {
            // arrange
            var error = "error";
            var failure = Result<string>.Failure(error).ToTask();

            // act
            var result = await failure.ToResultAsync();

            // assert
            var res = result.GetError();
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeFalse();
                res.Message.Should().Be(error);
            }
        }
    }
}