using FluentAssertions;
using FluentAssertions.Execution;
using Results;
using Results.Errors;
using Xunit;

namespace ResultTests;

public class ToResult
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
        var res = result.Match(() => "", e => e.Message);
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeFalse();
            res.Should().Be(error);
        }
    }
}