namespace Hechtag.JustResults.Tests;

public sealed class FromBool
{
    public sealed class Sync
    {
        [Fact]
        public void Success()
        {
            // arrange
            var inputBool = true;

            // act
            var success = inputBool.FromBool("Failed");

            // assert
            using (new AssertionScope())
            {
                success.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var inputBool = false;

            // act
            var failure = inputBool.FromBool("Failed");

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                failure.GetError().Message.Should().Be("Failed");
            }
        }
    }
    public sealed class Async
    {
        [Fact]
        public async Task Success()
        {
            // arrange
            var inputBool = true.ToTask();

            // act
            var success = await inputBool.FromBool("Failed");

            // assert
            using (new AssertionScope())
            {
                success.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Failure()
        {
            // arrange
            var inputBool = false.ToTask();

            // act
            var failure = await inputBool.FromBool("Failed");

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                failure.GetError().Message.Should().Be("Failed");
            }
        }
    }
}