using FluentAssertions;
using FluentAssertions.Execution;
using Results;
using Results.Errors;
using Xunit;

namespace ResultTests;

public class Tap
{
    [Fact]
    public void Success()
    {
        // arrange
        var input = "input";
        var success_count = 0;
        var success_data = "";
        var error_count = 0;
        var error_data = "";

        void SuccessAction(string data)
        {
            success_count++;
            success_data = data;
        }

        void ErrorAction(IError e)
        {
            error_count++;
            error_data = e.Message;
        }

        var success = Result<string>.Success(input);
        

        // act
        success.Tap(SuccessAction, ErrorAction);

        // assert
        using (new AssertionScope())
        {
            success.IsSuccess.Should().BeTrue();

            success_count.Should().Be(1);
            success_data.Should().Be(input);

            error_count.Should().Be(0);
            error_data.Should().Be("");
        }
    }

    [Fact]
    public void Failure()
    {
        // arrange
        var error = "error";
        var success_count = 0;
        var success_data = "";
        var error_count = 0;
        var error_data = "";

        void SuccessAction(string data)
        {
            success_count++;
            success_data = data;
        }

        void ErrorAction(IError e)
        {
            error_count++;
            error_data = e.Message;
        }

        var success = Result<string>.Failure(error);
        

        // act
        success.Tap(SuccessAction, ErrorAction);

        // assert
        using (new AssertionScope())
        {
            success.IsSuccess.Should().BeFalse();

            success_count.Should().Be(0);
            success_data.Should().Be("");

            error_count.Should().Be(1);
            error_data.Should().Be(error);
        }
    }
}