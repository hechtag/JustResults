namespace Hechtag.JustResults.Tests;

public sealed class Flatten
{
    public sealed class Sync
    {
        public sealed class With_Content
        {
            [Fact]
            public void Both_Success()
            {
                // arrange
                var input = "input";
                var success = Result<Result<string>>.Success(Result<string>.Success(input));

                // act
                var res = success.Flatten();

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().NotBeNull().And.Be("input");
                }
            }

            [Fact]
            public void Outer_Success()
            {
                // arrange
                var failure = Result<Result<string>>.Success(Result<string>.Failure("error"));

                // act
                var res = failure.Flatten();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Outer_Failure()
            {
                // arrange
                var errorInput = "error";
                var failure = Result<Result<string>>.Failure(errorInput);

                // act
                var res = failure.Flatten();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public void Both_Success()
            {
                // arrange
                var success = Result<Result>.Success(Result.Success());

                // act
                var res = success.Flatten();

                // assert
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                }
            }

            [Fact]
            public void Outer_Success()
            {
                // arrange
                var failure = Result<Result>.Success(Result.Failure("error"));

                // act
                var res = failure.Flatten();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Outer_Failure()
            {
                // arrange
                var errorInput = "error";
                var errorResult = Result<Result>.Failure(errorInput);

                // act
                var res = errorResult.Flatten();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }
        }
    }

    public sealed class Async
    {
        public sealed class With_Content
        {
            [Fact]
            public async Task Both_Success()
            {
                // arrange
                var input = "input";
                var success = Result<Result<string>>.Success(Result<string>.Success(input)).ToTask();

                // act
                var res = await success.FlattenAsync();

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().NotBeNull().And.Be("input");
                }
            }

            [Fact]
            public async Task Outer_Success()
            {
                // arrange
                var failure = Result<Result<string>>.Success(Result<string>.Failure("error")).ToTask();

                // act
                var res = await failure.FlattenAsync();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Outer_Failure()
            {
                // arrange
                var errorInput = "error";
                var failure = Result<Result<string>>.Failure(errorInput).ToTask();

                // act
                var res = await failure.FlattenAsync();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public async Task Both_Success()
            {
                // arrange
                var success = Result<Result>.Success(Result.Success()).ToTask();

                // act
                var res = await success.FlattenAsync();

                // assert
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                }
            }

            [Fact]
            public async Task Outer_Success()
            {
                // arrange
                var failure = Result<Result>.Success(Result.Failure("error")).ToTask();

                // act
                var res = await failure.FlattenAsync();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Outer_Failure()
            {
                // arrange
                var errorInput = "error";
                var errorResult = Result<Result>.Failure(errorInput).ToTask();

                // act
                var res = await errorResult.FlattenAsync();

                // assert
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }
        }

    }
}