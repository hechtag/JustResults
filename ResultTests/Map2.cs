using Results;
using Results.Errors;

namespace ResultTests;

public sealed class Map2
{
    public sealed class Sync
    {
        public sealed class With_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var input1 = "input1";
                var input2 = "input2";
                var success1 = Result<string>.Success(input1);
                var success2 = Result<string>.Success(input2);

                // act
                var res = Result<string>
                    .Map2(
                        success1,
                        success2,
                        (d1, d2) => d1 + d2 + " map");

                // assert
                var result_message = res.Match(data => data, err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("input1input2 map");
                }
            }

            [Fact]
            public void Failure_First()
            {
                // arrange
                var error = "error";
                var input = "input";
                var failure = Result<string>.Failure(error);
                var success = Result<string>.Success(input);

                // act
                var res = Result<string>
                    .Map2(
                        failure,
                        success,
                        (d1, d2) => d1 + d2 + " map");
                // assert
                var result_message = res.Match(data => data, err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Should().Be("error");
                }
            }

            [Fact]
            public void Failure_Second()
            {
                // arrange
                var input = "input";
                var error = "error";
                var success = Result<string>.Success(input);
                var failure = Result<string>.Failure(error);

                // act
                var res = Result<string>
                    .Map2(
                        success,
                        failure,
                        (d1, d2) => d1 + d2 + " map");
                // assert
                var result_message = res.Match(data => data, err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Should().Be("error");
                }
            }

            [Fact]
            public void Failure_Both()
            {
                // arrange
                var error1 = "error1";
                var error2 = "error2";
                var failure1 = Result<string>.Failure(error1);
                var failure2 = Result<string>.Failure(error2);

                // act
                var res = Result<string>
                    .Map2(
                        failure1,
                        failure2,
                        (d1, d2) => d1 + d2 + " map");
                // assert
                var result_error = res.Match(data => TextError.Create(""), err => err);
                var errors = result_error as CompositeError;

                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_error.Message.Should().Be("There are 2 Errors. ");

                    errors.Should().NotBeNull();
                    errors!.Errors.Should().HaveCount(2);
                    errors.Errors.First().Message.Should().Be(error1);
                    errors.Errors.Skip(1).First().Message.Should().Be(error2);
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public void Success()
            {
                // arrange
                var success1 = Result.Success();
                var success2 = Result.Success();

                // act
                var res = Result.Map2(success1, success2);

                // assert
                var result_message = res.Match(() => "map", err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                    result_message.Should().Be("map");
                }
            }

            [Fact]
            public void Failure_First()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error);
                var success = Result.Success();

                // act
                var res = Result.Map2(failure, success);
                // assert
                var result_message = res.Match(() => "map", err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Should().Be("error");
                }
            }

            [Fact]
            public void Failure_Second()
            {
                // arrange
                var error = "error";
                var success = Result.Success();
                var failure = Result.Failure(error);

                // act
                var res = Result.Map2(success, failure);
                // assert
                var result_message = res.Match(() => "", err => err.Message);
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Should().Be("error");
                }
            }

            [Fact]
            public void Failure_Both()
            {
                // arrange
                var error1 = "error1";
                var error2 = "error2";
                var failure1 = Result.Failure(error1);
                var failure2 = Result.Failure(error2);

                // act
                var res = Result.Map2(failure1, failure2);
                // assert
                var result_error = res.Match(() => TextError.Create(""), err => err);
                var errors = result_error as CompositeError;

                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_error.Message.Should().Be("There are 2 Errors. ");

                    errors.Should().NotBeNull();
                    errors!.Errors.Should().HaveCount(2);
                    errors.Errors.First().Message.Should().Be(error1);
                    errors.Errors.Skip(1).First().Message.Should().Be(error2);
                }
            }
        }
    }

    public sealed class AsSync
    {
        public sealed class With_Content
        {
            public sealed class Sync_start
            {
                [Fact]
                public async Task Success()
                {
                    // arrange
                    var input1 = "input1";
                    var input2 = "input2";
                    var success1 = Result<string>.Success(input1);
                    var success2 = Result<string>.Success(input2);

                    // act
                    var res = await Result<string>
                        .Map2(
                            success1,
                            success2,
                            (d1, d2) => (d1 + d2 + " map").ToTask());

                    // assert
                    var result_message = res.Match(data => data, err => err.Message);
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeTrue();
                        result_message.Should().Be("input1input2 map");
                    }
                }

                [Fact]
                public async Task Failure_First()
                {
                    // arrange
                    var error = "error";
                    var input = "input";
                    var failure = Result<string>.Failure(error);
                    var success = Result<string>.Success(input);

                    // act
                    var res = await Result<string>
                        .Map2(
                            failure,
                            success,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_message = res.Match(data => data, err => err.Message);
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_message.Should().Be("error");
                    }
                }

                [Fact]
                public async Task Failure_Second()
                {
                    // arrange
                    var input = "input";
                    var error = "error";
                    var success = Result<string>.Success(input);
                    var failure = Result<string>.Failure(error);

                    // act
                    var res = await Result<string>
                        .Map2(
                            success,
                            failure,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_message = res.Match(data => data, err => err.Message);
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_message.Should().Be("error");
                    }
                }

                [Fact]
                public async Task Failure_Both()
                {
                    // arrange
                    var error1 = "error1";
                    var error2 = "error2";
                    var failure1 = Result<string>.Failure(error1);
                    var failure2 = Result<string>.Failure(error2);

                    // act
                    var res = await Result<string>
                        .Map2(
                            failure1,
                            failure2,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_error = res.Match(data => TextError.Create(""), err => err);
                    var errors = result_error as CompositeError;

                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_error.Message.Should().Be("There are 2 Errors. ");

                        errors.Should().NotBeNull();
                        errors!.Errors.Should().HaveCount(2);
                        errors.Errors.First().Message.Should().Be(error1);
                        errors.Errors.Skip(1).First().Message.Should().Be(error2);
                    }
                }
            }

            public sealed class Async_start
            {
                [Fact]
                public async Task Success()
                {
                    // arrange
                    var input1 = "input1";
                    var input2 = "input2";
                    var success1 = Result<string>.Success(input1).ToTask();
                    var success2 = Result<string>.Success(input2).ToTask();

                    // act
                    var res = await Result<string>
                        .Map2(
                            success1,
                            success2,
                            (d1, d2) => (d1 + d2 + " map").ToTask());

                    // assert
                    var result_message = res.Match(data => data, err => err.Message);
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeTrue();
                        result_message.Should().Be("input1input2 map");
                    }
                }

                [Fact]
                public async Task Failure_First()
                {
                    // arrange
                    var error = "error";
                    var input = "input";
                    var failure = Result<string>.Failure(error).ToTask();
                    var success = Result<string>.Success(input).ToTask();

                    // act
                    var res = await Result<string>
                        .Map2(
                            failure,
                            success,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_message = res.GetError();
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_message.Message.Should().Be("error");
                    }
                }

                [Fact]
                public async Task Failure_Second()
                {
                    // arrange
                    var input = "input";
                    var error = "error";
                    var success = Result<string>.Success(input).ToTask();
                    var failure = Result<string>.Failure(error).ToTask();

                    // act
                    var res = await Result<string>
                        .Map2(
                            success,
                            failure,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_message = res.Match(data => data, err => err.Message);
                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_message.Should().Be("error");
                    }
                }

                [Fact]
                public async Task Failure_Both()
                {
                    // arrange
                    var error1 = "error1";
                    var error2 = "error2";
                    var failure1 = Result<string>.Failure(error1).ToTask();
                    var failure2 = Result<string>.Failure(error2).ToTask();

                    // act
                    var res = await Result<string>
                        .Map2(
                            failure1,
                            failure2,
                            (d1, d2) => (d1 + d2 + " map").ToTask());
                    // assert
                    var result_error = res.Match(data => TextError.Create(""), err => err);
                    var errors = result_error as CompositeError;

                    using (new AssertionScope())
                    {
                        res.IsSuccess.Should().BeFalse();
                        result_error.Message.Should().Be("There are 2 Errors. ");

                        errors.Should().NotBeNull();
                        errors!.Errors.Should().HaveCount(2);
                        errors.Errors.First().Message.Should().Be(error1);
                        errors.Errors.Skip(1).First().Message.Should().Be(error2);
                    }
                }
            }
        }

        public sealed class No_Content
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var success1 = Result.Success().ToTask();
                var success2 = Result.Success().ToTask();

                // act
                var res = await Result.Map2(success1, success2);

                // assert
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeTrue();
                }
            }

            [Fact]
            public async Task Failure_First()
            {
                // arrange
                var error = "error";
                var failure = Result.Failure(error).ToTask();
                var success = Result.Success().ToTask();

                // act
                var res = await Result.Map2(failure, success);
                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Failure_Second()
            {
                // arrange
                var error = "error";
                var success = Result.Success().ToTask();
                var failure = Result.Failure(error).ToTask();

                // act
                var res = await Result.Map2(success, failure);
                // assert
                var result_message = res.GetError();
                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_message.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Failure_Both()
            {
                // arrange
                var error1 = "error1";
                var error2 = "error2";
                var failure1 = Result.Failure(error1).ToTask();
                var failure2 = Result.Failure(error2).ToTask();

                // act
                var res = await Result.Map2(failure1, failure2);
                // assert
                var result_error = res.GetError();
                var errors = result_error as CompositeError;

                using (new AssertionScope())
                {
                    res.IsSuccess.Should().BeFalse();
                    result_error.Message.Should().Be("There are 2 Errors. ");

                    errors.Should().NotBeNull();
                    errors!.Errors.Should().HaveCount(2);
                    errors.Errors.First().Message.Should().Be(error1);
                    errors.Errors.Skip(1).First().Message.Should().Be(error2);
                }
            }
        }
    }
}