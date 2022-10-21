using Results;

namespace ResultTests;

public sealed class Map
{
    public sealed class Sync
    {
        public sealed class With_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var input = "input";
                var success = Result<string>.Success(input);

                // act
                var res = success.Map(data => data + " map");

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("input map");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var failure = Result<string>.Failure(error);

                // act
                var res = failure.Map(data => data + " map");

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
            public void Success()
            {
                // arrange
                var success = Result.Success();

                // act
                var res = success.Map(() => "map");

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("map");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error);

                // act
                var res = failure.Map(() => "map");

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

    public sealed class Async
    {
        public sealed class With_Content
        {
            [Fact]
            public async Task Success_with_sync_start()
            {
                // arrange
                var input = "input";
                var success = Result<string>.Success(input);
                var func = (string d) => Task.FromResult(d + " map");

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("input map");
                }
            }

            [Fact]
            public async Task Failure_with_sync_start()
            {
                // arrange
                var error = "error";
                var failure = Result<string>.Failure(error);
                var func = (string d) => Task.FromResult(d + " map");

                // act
                var res = await failure.Map(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Success_with_async_start()
            {
                // arrange
                var input = "input";
                var success = Result<string>.Success(input).ToTask();
                var func = (string d) => d + " map";

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("input map");
                }
            }

            [Fact]
            public async Task Failure_with_async_start()
            {
                // arrange
                var error = "error";
                var failure = Result<string>.Failure(error).ToTask();
                var func = (string d) => d + " map";

                // act
                var res = await failure.Map(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Success_with_both_async()
            {
                // arrange
                var input = "input";
                var success = Result<string>.Success(input).ToTask();
                var func = (string d) => Task.FromResult(d + " map");

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("input map");
                }
            }

            [Fact]
            public async Task Failure_with_both_async()
            {
                // arrange
                var error = "error";
                var failure = Result<string>.Failure(error).ToTask();
                var func = (string d) => Task.FromResult(d + " map");

                // act
                var res = await failure.Map(func);

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
            public async Task Success_with_sync_start()
            {
                // arrange
                var success = Result.Success();
                var func = () => Task.FromResult("map");

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("map");
                }
            }

            [Fact]
            public async Task Failure_with_sync_start()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error);
                var func = () => Task.FromResult("map");

                // act
                var res = await failure.Map(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Success_with_async_start()
            {
                // arrange
                var success = Result.Success().ToTask();
                var func = () => "map";

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("map");
                }
            }

            [Fact]
            public async Task Failure_with_async_start()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error).ToTask();
                var func = () =>  "map";

                // act
                var res = await failure.Map(func);

                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Success_with_both_async()
            {
                // arrange
                var success = Result.Success().ToTask();
                var func = () => Task.FromResult("map");

                // act
                var res = await success.Map(func);

                // assert
                var result_message = res.GetValue();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("map");
                }
            }

            [Fact]
            public async Task Failure_with_both_async()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error).ToTask();
                var func = () => Task.FromResult("map");

                // act
                var res = await failure.Map(func);

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