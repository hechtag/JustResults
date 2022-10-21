using Results;

namespace ResultTests.Extensions;

public sealed class IEnumerable
{
    public sealed class Where_Success
    {
        public sealed class Sync
        {
            [Fact]
            public void Filter_Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Success("success1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Success("success2"),
                };

                // act
                var successList = list.WhereSuccess();

                // assert
                using (new AssertionScope())
                {
                    successList.Should().HaveCount(2);
                    successList.First().Should().Be("success1");
                    successList.Last().Should().Be("success2");
                }
            }

            [Fact]
            public void Empty_List()
            {
                // arrange
                var list = new List<Result<string>>();

                // act
                var successList = list.WhereSuccess();

                // assert
                using (new AssertionScope())
                {
                    successList.Should().HaveCount(0);
                }
            }
        }

        public sealed class Async
        {
            [Fact]
            public async Task Filter_Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Success("success1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Success("success2"),
                }.AsEnumerable().ToTask();

                // act
                var successList = await list.WhereSuccess();

                // assert
                using (new AssertionScope())
                {
                    successList.Should().HaveCount(2);
                    successList.First().Should().Be("success1");
                    successList.Last().Should().Be("success2");
                }
            }

            [Fact]
            public async Task Empty_List()
            {
                // arrange
                var list = new List<Result<string>>().AsEnumerable().ToTask();

                // act
                var successList = await list.WhereSuccess();

                // assert
                using (new AssertionScope())
                {
                    successList.Should().HaveCount(0);
                }
            }
        }
    }

    public sealed class Where_Failure
    {
        public sealed class Sync
        {
            [Fact]
            public void Filter_Errors()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Success("success1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Success("success2"),
                };

                // act
                var failureList = list.WhereFailure();

                // assert
                using (new AssertionScope())
                {
                    failureList.Should().HaveCount(2);
                    failureList.First().Message.Should().Be("error1");
                    failureList.Last().Message.Should().Be("error2");
                }
            }

            [Fact]
            public void Empty_List()
            {
                // arrange
                var list = new List<Result<string>>();

                // act
                var failureList = list.WhereFailure();

                // assert
                using (new AssertionScope())
                {
                    failureList.Should().HaveCount(0);
                }
            }
        }

        public sealed class Async
        {
            [Fact]
            public async Task Filter_Errors()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Success("success1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Success("success2"),
                }.AsEnumerable().ToTask();

                // act
                var failureList = await list.WhereFailure();

                // assert
                using (new AssertionScope())
                {
                    failureList.Should().HaveCount(2);
                    failureList.First().Message.Should().Be("error1");
                    failureList.Last().Message.Should().Be("error2");
                }
            }

            [Fact]
            public async Task Empty_List()
            {
                // arrange
                var list = new List<Result<string>>().AsEnumerable().ToTask();

                // act
                var failureList = await list.WhereFailure();

                // assert
                using (new AssertionScope())
                {
                    failureList.Should().HaveCount(0);
                }
            }
        }
    }

    public sealed class Sequence_Apply
    {
        public sealed class Sync
        {
            [Fact]
            public void Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success1"),
                    Result<string>.Success("success2"),
                };

                // act
                var successResult = list.SequenceApply();

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public void One_Failure()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error"),
                };

                // act
                var failureResult = list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Two_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                };

                // act
                var failureResult = list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 2 Errors. ");
                    error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2]");
                }
            }

            [Fact]
            public void Three_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Failure("error3"),
                };

                // act
                var failureResult = list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 3 Errors. ");
                    error!.Display.Should()
                        .Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3]");
                }
            }
        }
        public sealed class Async
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success1"),
                    Result<string>.Success("success2"),
                }.AsEnumerable().ToTask();

                // act
                var successResult = await list.SequenceApply();

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public async Task One_Failure()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Two_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 2 Errors. ");
                    error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2]");
                }
            }

            [Fact]
            public async Task Three_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Failure("error3"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceApply();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 3 Errors. ");
                    error!.Display.Should()
                        .Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3]");
                }
            }
        }
    }

    public sealed class Sequence_Bind
    {
        public sealed class Sync
        {
            [Fact]
            public void Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success1"),
                    Result<string>.Success("success2"),
                };

                // act
                var successResult = list.SequenceBind();

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public void One_Failure()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error"),
                };

                // act
                var failureResult = list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Two_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                };

                // act
                var failureResult = list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }

            [Fact]
            public void Three_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Failure("error3"),
                };

                // act
                var failureResult = list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }
        }
        public sealed class Async
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success1"),
                    Result<string>.Success("success2"),
                }.AsEnumerable().ToTask();

                // act
                var successResult = await list.SequenceBind();

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public async Task One_Failure()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Two_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Success("success"),
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }

            [Fact]
            public async Task Three_Failures()
            {
                // arrange
                var list = new List<Result<string>>
                {
                    Result<string>.Failure("error1"),
                    Result<string>.Failure("error2"),
                    Result<string>.Failure("error3"),
                }.AsEnumerable().ToTask();

                // act
                var failureResult = await list.SequenceBind();

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }
        }
    }

    public sealed class Traverse_Apply
    {
        public sealed class Sync
        {
            [Fact]
            public void Success()
            {
                // arrange
                var list = new List<string>
                {
                    "success1",
                    "success2",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var successResult = list.TraverseApply(TestFunc);

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public void One_Failure()
            {
                // arrange
                var list = new List<string>
                {
                    "success",
                    "error"
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var failureResult = list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Two_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 2 Errors. ");
                    error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2]");
                }
            }

            [Fact]
            public void Three_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                    "error3",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 3 Errors. ");
                    error!.Display.Should()
                        .Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3]");
                }
            }
        }
        public sealed class Async
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var list = new List<string>
                {
                    "success1",
                    "success2",
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var successResult = await list.TraverseApply(TestFunc);

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public async Task One_Failure()
            {
                // arrange
                var list = new List<string>
                {
                    "success",
                    "error"
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var failureResult = await list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Two_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = await list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 2 Errors. ");
                    error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2]");
                }
            }

            [Fact]
            public async Task Three_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                    "error3",
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = await list.TraverseApply(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("There are 3 Errors. ");
                    error!.Display.Should()
                        .Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3]");
                }
            }
        }
    }

    public sealed class Traverse_Bind
    {
        public sealed class Sync
        {
            [Fact]
            public void Success()
            {
                // arrange
                var list = new List<string>
                {
                    "success1",
                    "success2",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var successResult = list.TraverseBind(TestFunc);

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public void One_Failure()
            {
                // arrange
                var list = new List<string>
                {
                    "success",
                    "error"
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var failureResult = list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public void Two_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }

            [Fact]
            public void Three_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                    "error3",
                };

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }
        }
        public sealed class Async
        {
            [Fact]
            public async Task Success()
            {
                // arrange
                var list = new List<string>
                {
                    "success1",
                    "success2",
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var successResult = await list.TraverseBind(TestFunc);

                // assert
                var successList = successResult.GetValue();
                using (new AssertionScope())
                {
                    successResult.IsSuccess.Should().BeTrue();

                    successList.Should().HaveCount(2);
                    successList!.First().Should().Be("success1");
                    successList!.Last().Should().Be("success2");
                }
            }

            [Fact]
            public async Task One_Failure()
            {
                // arrange
                var list = new List<string>
                {
                    "success",
                    "error"
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure("error");

                // act
                var failureResult = await list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error");
                }
            }

            [Fact]
            public async Task Two_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                } .AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = await list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }

            [Fact]
            public async Task Three_Failures()
            {
                // arrange
                var list = new List<string>
                {
                    "error1",
                    "error2",
                    "error3",
                }.AsEnumerable().ToTask();

                Result<string> TestFunc(string input) =>
                    input.Contains("success")
                        ? Result<string>.Success(input)
                        : Result<string>.Failure(input);

                // act
                var failureResult = await list.TraverseBind(TestFunc);

                // assert
                var error = failureResult.GetError();
                using (new AssertionScope())
                {
                    failureResult.IsSuccess.Should().BeFalse();

                    error.Should().NotBeNull();
                    error!.Message.Should().Be("error1");
                }
            }
        }
    }
}