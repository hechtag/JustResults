using FluentAssertions;
using FluentAssertions.Execution;
using Results;
using Xunit;

namespace ResultTests.Extensions;

public class FailIfNull
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