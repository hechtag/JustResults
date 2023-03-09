namespace Hechtag.JustResults.Tests;

public sealed class Bind
{
    public sealed class Sync
    {
        public sealed class With_Content
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
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().NotBeNull().And.Be("input bind");
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
                var error = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    error.Should().NotBeNull();
                    error!.Message.Should().Be("input error");
                }
            }

            [Fact]
            public void Failure_with_Success_Bind()
            {
                // arrange
                var errorInput = "error";
                var success = Result<string>.Failure(errorInput);

                // act
                var res = success.Bind(data => Result<string>.Success(data + " bind"));

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
            public void Failure_with_Failure_Bind()
            {
                // arrange
                var errorInput = "error";
                var errorResult = Result<string>.Failure(errorInput);

                // act
                var res = errorResult.Bind(data => Result<string>.Failure(data + " bind"));

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
            public void Success_with_Success_Bind()
            {
                // arrange
                var success = Result.Success();

                // act
                var res = success.Bind(() => Result<string>.Success("bind"));

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("bind");
                }
            }

            [Fact]
            public void Success_with_Failure_Bind()
            {
                // arrange
                var success = Result.Success();

                // act
                var res = success.Bind(() => Result<string>.Failure("error"));

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
            public void Failure_with_Success_Bind()
            {
                // arrange
                var errorInput = "error";
                var errorResult = Result.Failure(errorInput);

                // act
                var res = errorResult.Bind(() => Result<string>.Success("bind"));

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
            public void Failure_with_Failure_Bind()
            {
                // arrange
                var errorInput = "error";
                var errorResult = Result.Failure(errorInput);

                // act
                var res = errorResult.Bind(() => Result<string>.Failure("bind"));

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
            public sealed class With_sync_start
            {
                [Fact]
                public async Task Success_with_Success_Bind()
                {
                    // arrange
                    var input = "input";
                    var success = Result<string>.Success(input);

                    // act
                    var res = await success.Bind(data => Result<string>.Success(data + " bind").ToTask());

                    // assert
                    var result_message = res.GetValue();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeTrue();
                        result_message.Should().NotBeNull().And.Be("input bind");
                    }
                }

                [Fact]
                public async Task Success_with_Failure_Bind()
                {
                    // arrange
                    var input = "input";
                    var success = Result<string>.Success(input);

                    // act
                    var res = await success.Bind(data => Result<string>.Failure(data + " error").ToTask());

                    // assert
                    var error = res.GetError();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        error.Should().NotBeNull();
                        error!.Message.Should().Be("input error");
                    }
                }

                [Fact]
                public async Task Failure_with_Success_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var success = Result<string>.Failure(errorInput);

                    // act
                    var res = await success.Bind(data => Result<string>.Success(data + " bind").ToTask());

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
                public async Task Failure_with_Failure_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var errorResult = Result<string>.Failure(errorInput);

                    // act
                    var res = await errorResult.Bind(data => Result<string>.Failure(data + " bind").ToTask());

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
            public sealed class With_async_start
            {
                [Fact]
                public async Task Success_with_Success_Bind()
                {
                    // arrange
                    var input = "input";
                    var success = Result<string>.Success(input).ToTask();

                    // act
                    var res = await success.BindAsync(data => Result<string>.Success(data + " bind").ToTask());

                    // assert
                    var result_message = res.GetValue();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeTrue();
                        result_message.Should().NotBeNull().And.Be("input bind");
                    }
                }

                [Fact]
                public async Task Success_with_Failure_Bind()
                {
                    // arrange
                    var input = "input";
                    var success = Result<string>.Success(input).ToTask();

                    // act
                    var res = await success.BindAsync(data => Result<string>.Failure(data + " error").ToTask());

                    // assert
                    var error = res.GetError();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        error.Should().NotBeNull();
                        error!.Message.Should().Be("input error");
                    }
                }

                [Fact]
                public async Task Failure_with_Success_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var success = Result<string>.Failure(errorInput).ToTask();

                    // act
                    var res = await success.BindAsync(data => Result<string>.Success(data + " bind").ToTask());

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
                public async Task Failure_with_Failure_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var errorResult = Result<string>.Failure(errorInput).ToTask();

                    // act
                    var res = await errorResult.BindAsync(data => Result<string>.Failure(data + " bind").ToTask());

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

        public sealed class No_Content
        {
            public sealed class With_sync_start
            {

                [Fact]
                public async Task Success_with_Success_Bind()
                {
                    // arrange
                    var success = Result.Success();

                    // act
                    var res = await success.Bind(() => Result<string>.Success("bind").ToTask());

                    // assert
                    var result_message = res.GetValue();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeTrue();
                        result_message.Should().Be("bind");
                    }
                }

                [Fact]
                public async Task Success_with_Failure_Bind()
                {
                    // arrange
                    var success = Result.Success();

                    // act
                    var res = await success.Bind(() => Result<string>.Failure("error").ToTask());

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
                public async Task Failure_with_Success_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var errorResult = Result.Failure(errorInput);

                    // act
                    var res = await errorResult.Bind(() => Result<string>.Success("bind").ToTask());

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
                public async Task Failure_with_Failure_Bind()
                {
                    // arrange
                    var errorInput = "error";
                    var errorResult = Result.Failure(errorInput);

                    // act
                    var res = await errorResult.Bind(() => Result<string>.Failure("bind").ToTask());

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
}