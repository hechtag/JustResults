using Results.Synchronous;

namespace ResultTests.Synchronous;

public class GetErrorAndGetValue
{
    [Fact]
    public void Success()
    {
        // arrange
        var input = "input";
        var success = Result<string>.Success(input);

        // act
        var value = success.GetValue();
        var error = success.GetError();

        // assert
        using (new AssertionScope())
        {
            success.IsSuccess.Should().BeTrue();
            value.Should().NotBeNull()
                .And.Be(input);
            
            error.Should().BeNull();
        }
    } 
    [Fact]
    public void Failure()
    {
        // arrange
        var errorText = "error";
        var success = Result<string>.Failure(errorText);

        // act
        var value = success.GetValue();
        var error = success.GetError();

        // assert
        using (new AssertionScope())
        {
            success.IsSuccess.Should().BeFalse();
            value.Should().BeNull();

            error.Should().NotBeNull();
            error!.Message.Should().Be(errorText);
        }
    }
}