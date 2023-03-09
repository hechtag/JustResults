using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults.Tests;

public sealed class TapBoth
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
                success.TapBoth(SuccessAction, ErrorAction);

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
                success.TapBoth(SuccessAction, ErrorAction);

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

        public sealed class No_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var success_count = 0;
                var error_count = 0;
                var error_data = "";

                void SuccessAction()
                {
                    success_count++;
                }

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result.Success();


                // act
                success.TapBoth(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);

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
                var error_count = 0;
                var error_data = "";

                void SuccessAction()
                {
                    success_count++;
                }

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var success = Result.Failure(error);


                // act
                success.TapBoth(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);

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

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

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
            public async Task Failure_sync_lambda()
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

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

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

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var input = "input";

                var success_count = 0;
                var success_data = "";
                var error_count = 0;
                var error_data = "";

                Task SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                    return Task.CompletedTask;
                }

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

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
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var success_data = "";
                var error_count = 0;
                var error_data = "";

                Task SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                    return Task.CompletedTask;
                }

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

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

        public sealed class No_Content
        {
            [Fact]
            public async Task Success_sync_lambda()
            {
                // arrange
                var success_count = 0;
                var error_count = 0;
                var error_data = "";

                void SuccessAction()
                {
                    success_count++;
                }

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_sync_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var error_count = 0;
                var error_data = "";

                void SuccessAction()
                {
                    success_count++;
                }

                void ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var success_count = 0;
                var error_count = 0;
                var error_data = "";

                Task SuccessAction()
                {
                    success_count++;
                    return Task.CompletedTask;
                }

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);

                    error_count.Should().Be(0);
                    error_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var error_count = 0;
                var error_data = "";

                Task SuccessAction()
                {
                    success_count++;
                    return Task.CompletedTask;
                }

                Task ErrorAction(IError e)
                {
                    error_count++;
                    error_data = e.Message;
                    return Task.CompletedTask;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction, ErrorAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);

                    error_count.Should().Be(1);
                    error_data.Should().Be(error);
                }
            }
        }
    }
}