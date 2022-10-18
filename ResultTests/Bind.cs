using FluentAssertions;
using FluentAssertions.Execution;
using Results;
using Xunit;

namespace ResultTests;

public class Bind
{
    [Fact]
    public void Success_with_Success_Bind()
    {
        // arrange
        var input = "input";
        var success = Result<string>.Success(input);

        // act
        var res = success.Bind(data => Result<string>.Success(data + " bind"));

        // assert
        var result_message = res.Match(data => data, err => err.Message);
        using (new AssertionScope())
        {
            res.IsSuccess.Should().BeTrue();
            result_message.Should().Be("input bind");
        }
    }
    
    [Fact]
    public void Success_with_Failure_Bind()
    {
        // arrange
        var input = "input";
        var success = Result<string>.Success(input);

        // act
        var res = success.Bind(data => Result<string>.Failure(data + " error"));

        // assert
        var result_message = res.Match(data => data, err => err.Message);
        using (new AssertionScope())
        {
            res.IsSuccess.Should().BeFalse();
            result_message.Should().Be("input error");
        }
    }

    [Fact]
    public void Failure_with_Success_Bind()
    {
        // arrange
        var error = "error";
        var success = Result<string>.Failure(error);

        // act
        var res = success.Bind(data => Result<string>.Success(data + " bind"));

        // assert
        var result_message = res.Match(data => data, err => err.Message);
        using (new AssertionScope())
        {
            res.IsSuccess.Should().BeFalse();
            result_message.Should().Be("error");
        }
    }
    
    [Fact]
    public void Failure_with_Failure_Bind()
    {
        // arrange
        var error = "error";
        var success = Result<string>.Failure(error);

        // act
        var res = success.Bind(data => Result<string>.Failure(data + " bind"));

        // assert
        var result_message = res.Match(data => data, err => err.Message);
        using (new AssertionScope())
        {
            res.IsSuccess.Should().BeFalse();
            result_message.Should().Be("error");
        }
    }
}