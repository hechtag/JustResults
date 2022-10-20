using Results.Synchronous;

namespace ResultTests.Synchronous;

public class Map
{
    public sealed class With_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var input = "input";
            var success = Result<string>.Success(input);

            // act
            var res = success.Map(data => data + " map");

            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeTrue();
                result_message.Should().Be("input map");
            }
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var error = "error";
            var failure = Result<string>.Failure(error);

            // act
            var res = failure.Map(data => data + " map");

            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }
    }  
    
    public sealed class No_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var success = Results.Synchronous.Result.Success();

            // act
            var res = success.Map(() => "map");

            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeTrue();
                result_message.Should().Be("map");
            }
        }

        [Fact]
        public void Failure()
        {
            // arrange
            var error = "error";
            var failure = Results.Synchronous.Result.Failure(error);

            // act
            var res = failure.Map(() => "map");

            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }
    }
}