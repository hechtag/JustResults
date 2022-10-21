using Results;

namespace ResultTests.Extensions;

public sealed class Query
{
    public sealed class Sync
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

    public sealed class Async
    {
        [Fact]
        public async Task Success()
        {
            // arrange + act
            var result = await
            (
                from a in Result<string>.Success("first").ToTask()
                from b in Result<string>.Success("second").ToTask()
                from c in Result<string>.Success("third").ToTask()
                from d in Result<string>.Success("fourth").ToTask()
                from e in Result<string>.Success("fifth").ToTask()
                select $"{a} {b} {c} {d} {e}");

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
        public async Task Failure()
        {
            // arrange + act
            var result = await
            (
                from a in Result<string>.Success("first").ToTask()
                from b in Result<string>.Success("second").ToTask()
                from c in Result<string>.Failure("third").ToTask()
                from d in Result<string>.Success("fourth").ToTask()
                from e in Result<string>.Success("fifth").ToTask()
                select $"{a} {b} {c} {d} {e}");

            // assert
            var error = result.GetError();
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("third");
            }
        }

        [Fact]
        public async Task Mixed_Success()
        {
            // arrange + act
            var result = await
            (
                from a in Result<string>.Success("first")
                from b in Result<string>.Success("second").ToTask()
                from c in Result<string>.Success("third")
                from d in Result<string>.Success("fourth").ToTask()
                from e in Result<string>.Success("fifth").ToTask()
                select $"{a} {b} {c} {d} {e}");

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
        public async Task Mixed_Failure()
        {
            // arrange + act
            var result = await
            (
                from a in Result<string>.Success("first").ToTask()
                from b in Result<string>.Success("second")
                from c in Result<string>.Failure("third").ToTask()
                from d in Result<string>.Success("fourth").ToTask()
                from e in Result<string>.Success("fifth")
                select $"{a} {b} {c} {d} {e}");

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
}