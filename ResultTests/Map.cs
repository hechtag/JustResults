using FluentAssertions;
using FluentAssertions.Execution;
using Results;
using Xunit;

namespace ResultTests;

public class Map
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
        var success = Result<string>.Failure(error);

        // act
        var res = success.Map(data => data + " map");

        // assert
        var result_message = res.Match(data => data, err => err.Message);
        using (new AssertionScope())
        {
            res.IsSuccess.Should().BeFalse();
            result_message.Should().Be("error");
        }
    }
}