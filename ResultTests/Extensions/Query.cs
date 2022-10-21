using Results;

namespace ResultTests.Extensions;

public sealed class Query
{
    [Fact]
    public void Success()
    {
        // arrange + act
        var result =
            from a in Result<string>.Success("first")
            from b in Result<string>.Success("second")
            from c in Result<string>.Success("third")
            from d in Result<string>.Success("fourth")
            from e in Result<string>.Success("fifth")
            select $"{a} {b} {c} {d} {e}";

        // assert
        var value = result.GetValue();
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            value.Should().NotBeNull();
            value.Should().Be("first second third fourth fifth");
        }
    }
    [Fact]
    public void Failure()
    {
        // arrange + act
        var result =
            from a in Result<string>.Success("first")
            from b in Result<string>.Success("second")
            from c in Result<string>.Failure("third")
            from d in Result<string>.Success("fourth")
            from e in Result<string>.Success("fifth")
            select $"{a} {b} {c} {d} {e}";

        // assert
        var error = result.GetError();
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeFalse();
            error.Should().NotBeNull();
            error!.Message.Should().Be("third");
        }
    }
}