using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults.Tests;

public sealed class TapError
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

                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result<string>.Success(input);


                // act
                success.TapError(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result<string>.Failure(error);


                // act
                success.TapError(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result.Success();


                // act
                success.TapError(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result.Failure(error);


                // act
                success.TapError(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }
        }
    }

    public sealed class Async
    {
        public sealed class With_Content
        {
            [Fact]
            public async Task Success_sync_lambda()
            {
                // arrange
                var input = "input";

                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_sync_lambda()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var input = "input";

                var error_count = 0;
                var error_data = "";

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public async Task Success_sync_lambda()
            {
                // arrange
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_sync_lambda()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var error_count = 0;
                var error_data = "";

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var error_count = 0;
                var error_data = "";

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapErrorAsync(ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }
        }
    }
}