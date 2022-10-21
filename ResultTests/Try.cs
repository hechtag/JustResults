using Results;

namespace ResultTests;

public sealed class Try
{
    public sealed class Sync
    {
        public sealed class With_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var func = () => "try";

                // act
                var res = Result<string>.Try(func);

                // assert
                var result_message = res.Match(data => data, err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("try");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var func = new Func<string>(() => throw new Exception("error"));

                // act
                var res = Result<string>.Try(func);

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
                var func = () => { };

                // act
                var res = Result.Try(func);

                // assert
                var result_message = res.Match(() => "try", err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("try");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var func = new Action(() => throw new Exception("error"));

                // act
                var res = Result.Try(func);

                // assert
                var result_message = res.Match(() => "try", err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Should().Be("error");
                }
            }
        }
    }

    public sealed class Async
    {
        public sealed class With_Content
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var func = () => Task.FromResult("try");

                // act
                var res = await Result<string>.Try(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("try");
                }
            }

            [Fact]
            public async Task Failure()
            {
                // arrange
                var func = new Func<Task<string>>(() => throw new Exception("error"));

                // act
                var res = await Result<string>.Try(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var func = () => Task.CompletedTask;

                // act
                var res = await Result.Try(func);

                // assert
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                }
            }

            [Fact]
            public async Task Failure()
            {
                // arrange
                var func = new Func<Task>(() => throw new Exception("error"));

                // act
                var res = await Result.Try(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }
        }
    }
}