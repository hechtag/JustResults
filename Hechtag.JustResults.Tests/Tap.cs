using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults.Tests;

public sealed class Tap
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

                void SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                }

                var success = Result<string>.Success(input);

                // act
                success.Tap(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                    success_data.Should().Be(input);
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var success_data = "";

                void SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                }

                var success = Result<string>.Failure(error);


                // act
                success.Tap(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
                    success_data.Should().Be("");
                }
            }

            [Fact]
            public void Mutate()
            {
                // arrange
                var mutable = Result<MutationHelper>.Success(new MutationHelper());

                // act
                mutable.Tap(m => m.MutableProp++)
                    .Tap(m => m.MutableProp++);

                // assert
                using (new AssertionScope())
                {
                    mutable.GetValue().MutableProp.Should().Be(2);
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

                void SuccessAction()
                {
                    success_count++;
                }

                var success = Result.Success();


                // act
                success.Tap(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                }
            }

            [Fact]
            public void Failure()
            {
                // arrange
                var error = "error";
                var success_count = 0;

                void SuccessAction()
                {
                    success_count++;
                }

                var success = Result.Failure(error);


                // act
                success.Tap(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
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

                void SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                }

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                    success_data.Should().Be(input);
                }
            }

            [Fact]
            public async Task Failure_sync_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var success_data = "";

                void SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                }

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
                    success_data.Should().Be("");
                }
            }

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var input = "input";

                var success_count = 0;
                var success_data = "";

                Task SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Success(input).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                    success_data.Should().Be(input);
                }
            }

            [Fact]
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;
                var success_data = "";

                Task SuccessAction(string data)
                {
                    success_count++;
                    success_data = data;
                    return Task.CompletedTask;
                }

                var successTask = Result<string>.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
                    success_data.Should().Be("");
                }
            }


            [Fact]
            public async Task Mutate()
            {
                // arrange
                var mutable = Result<MutationHelper>.Success(new MutationHelper()).ToTask();

                // act
                var result = await mutable.TapAsync(m => m.MutableProp++)
                    .TapAsync(m => m.MutableProp++);

                // assert
                using (new AssertionScope())
                {
                    result.GetValue().MutableProp.Should().Be(2);
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

                void SuccessAction()
                {
                    success_count++;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                }
            }

            [Fact]
            public async Task Failure_sync_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;

                void SuccessAction()
                {
                    success_count++;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
                }
            }

            [Fact]
            public async Task Success_async_lambda()
            {
                // arrange
                var success_count = 0;

                Task SuccessAction()
                {
                    success_count++;
                    return Task.CompletedTask;
                }

                var successTask = Result.Success().ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();

                    success_count.Should().Be(1);
                }
            }

            [Fact]
            public async Task Failure_async_lambda()
            {
                // arrange
                var error = "error";
                var success_count = 0;

                Task SuccessAction()
                {
                    success_count++;
                    return Task.CompletedTask;
                }

                var successTask = Result.Failure(error).ToTask();


                // act
                var success = await successTask.TapAsync(SuccessAction);

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();

                    success_count.Should().Be(0);
                }
            }
        }
    }
}

public class MutationHelper
{
    public int MutableProp { get; set; }
}