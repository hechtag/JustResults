using Results;

namespace ResultTests.Extensions;

public sealed class FailIfNull
{
    public sealed class Sync
    {
        [Fact]
        public void Input_is_null()
        {
            // arrange
            string? input = null;

            // act
            var failure = input.FailIfNull();
            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                result_message.Should().Be("The input was null. ");
            }
        }

        [Fact]
        public void Input_is_not_null()
        {
            // arrange
            string? input = "Some Value";

            // act
            var failure = input.FailIfNull();
            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeTrue();
                result_message.Should().Be("Some Value");
            }
        }
    }

    public sealed class Async
    {
        [Fact]
        public async Task Input_is_null()
        {
            // arrange
            var input = Task.FromResult(null as string);

            // act
            var failure = await input.FailIfNull();
            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                result_message.Should().Be("The input was null. ");
            }
        }

        [Fact]
        public async Task Input_is_not_null()
        {
            // arrange
            Task<string?> input = Task.FromResult("Some Value");

            // act
            var failure = await input.FailIfNull();
            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeTrue();
                result_message.Should().Be("Some Value");
            }
        }
    }
}