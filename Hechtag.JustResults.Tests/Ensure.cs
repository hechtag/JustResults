using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace Hechtag.JustResults.Tests;

public sealed class Ensure
{
    public sealed class Sync
    {
        public sealed class With_Content
        {
            [Fact]
            public void Success_EnsureSuccess()
            {
                // arrange
                var input = Result<string>.Success("input");

                // act
                var success = input.Ensure(_ => true,"Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();
                    success.GetValue().Should().Be("input");
                }
            }

            [Fact]
            public void Success_EnsureFailure()
            {
                // arrange
                var input = Result<string>.Success("input");

                // act
                var success = input.Ensure(_ => false,"Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Error");
                }
            }
            
            [Fact]
            public void Failure_EnsureFailure()
            {
                // arrange
                var input = Result<string>.Failure("Failure");

                // act
                var success = input.Ensure(_ => false,"Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Failure");
                }
            }
            
            [Fact]
            public void Failure_EnsureSuccess()
            {
                // arrange
                var input = Result<string>.Failure("Failure");

                // act
                var success = input.Ensure(_ => true,"Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Failure");
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public void Success_EnsureSuccess()
            {
                // arrange
                var input = Result.Success();

                // act
                var success = input.Ensure(() => true, "Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeTrue();
                }
            }

            [Fact]
            public void Success_EnsureFailure()
            {
                // arrange
                var input = Result.Success();

                // act
                var success = input.Ensure(() => false, "Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Error");
                }
            }

            [Fact]
            public void Failure_EnsureFailure()
            {
                // arrange
                var input = Result.Failure("Failure");

                // act
                var success = input.Ensure(() => false, "Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Failure");
                }
            }

            [Fact]
            public void Failure_EnsureSuccess()
            {
                // arrange
                var input = Result.Failure("Failure");

                // act
                var success = input.Ensure(() => true, "Error");

                // assert
                using (new AssertionScope())
                {
                    success.IsSuccess.Should().BeFalse();
                    success.GetError().Message.Should().Be("Failure");
                }
            }

        }
    }

    //public sealed class Async
    //{
    //    public sealed class With_Content
    //    {
    //        [Fact]
    //        public async Task Success_sync_lambda()
    //        {
    //            // arrange
    //            var input = "input";

    //            var success_count = 0;
    //            var success_data = "";

    //            void SuccessAction(string data)
    //            {
    //                success_count++;
    //                success_data = data;
    //            }

    //            var successTask = Result<string>.Success(input).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeTrue();

    //                success_count.Should().Be(1);
    //                success_data.Should().Be(input);
    //            }
    //        }

    //        [Fact]
    //        public async Task Failure_sync_lambda()
    //        {
    //            // arrange
    //            var error = "error";
    //            var success_count = 0;
    //            var success_data = "";

    //            void SuccessAction(string data)
    //            {
    //                success_count++;
    //                success_data = data;
    //            }

    //            var successTask = Result<string>.Failure(error).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeFalse();

    //                success_count.Should().Be(0);
    //                success_data.Should().Be("");
    //            }
    //        }

    //        [Fact]
    //        public async Task Success_async_lambda()
    //        {
    //            // arrange
    //            var input = "input";

    //            var success_count = 0;
    //            var success_data = "";

    //            Task SuccessAction(string data)
    //            {
    //                success_count++;
    //                success_data = data;
    //                return Task.CompletedTask;
    //            }

    //            var successTask = Result<string>.Success(input).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeTrue();

    //                success_count.Should().Be(1);
    //                success_data.Should().Be(input);
    //            }
    //        }

    //        [Fact]
    //        public async Task Failure_async_lambda()
    //        {
    //            // arrange
    //            var error = "error";
    //            var success_count = 0;
    //            var success_data = "";

    //            Task SuccessAction(string data)
    //            {
    //                success_count++;
    //                success_data = data;
    //                return Task.CompletedTask;
    //            }

    //            var successTask = Result<string>.Failure(error).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeFalse();

    //                success_count.Should().Be(0);
    //                success_data.Should().Be("");
    //            }
    //        }


    //        [Fact]
    //        public async Task Mutate()
    //        {
    //            // arrange
    //            var mutable = Result<MutationHelper>.Success(new MutationHelper()).ToTask();

    //            // act
    //            var result = await mutable.EnsureAsync(m => m.MutableProp++)
    //                .EnsureAsync(m => m.MutableProp++);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                result.GetValue().MutableProp.Should().Be(2);
    //            }
    //        }
    //    }

    //    public sealed class No_Content
    //    {
    //        [Fact]
    //        public async Task Success_sync_lambda()
    //        {
    //            // arrange
    //            var success_count = 0;

    //            void SuccessAction()
    //            {
    //                success_count++;
    //            }

    //            var successTask = Result.Success().ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeTrue();

    //                success_count.Should().Be(1);
    //            }
    //        }

    //        [Fact]
    //        public async Task Failure_sync_lambda()
    //        {
    //            // arrange
    //            var error = "error";
    //            var success_count = 0;

    //            void SuccessAction()
    //            {
    //                success_count++;
    //            }

    //            var successTask = Result.Failure(error).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeFalse();

    //                success_count.Should().Be(0);
    //            }
    //        }

    //        [Fact]
    //        public async Task Success_async_lambda()
    //        {
    //            // arrange
    //            var success_count = 0;

    //            Task SuccessAction()
    //            {
    //                success_count++;
    //                return Task.CompletedTask;
    //            }

    //            var successTask = Result.Success().ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeTrue();

    //                success_count.Should().Be(1);
    //            }
    //        }

    //        [Fact]
    //        public async Task Failure_async_lambda()
    //        {
    //            // arrange
    //            var error = "error";
    //            var success_count = 0;

    //            Task SuccessAction()
    //            {
    //                success_count++;
    //                return Task.CompletedTask;
    //            }

    //            var successTask = Result.Failure(error).ToTask();


    //            // act
    //            var success = await successTask.EnsureAsync(SuccessAction);

    //            // assert
    //            using (new AssertionScope())
    //            {
    //                success.IsSuccess.Should().BeFalse();

    //                success_count.Should().Be(0);
    //            }
    //        }
    //    }
    //}
}
