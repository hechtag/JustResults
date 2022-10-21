using Results;

namespace ResultTests.Extensions;

public sealed class MapAndApply
{
    public sealed class Apply_tow_params
    {
        [Fact]
        public void Success()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var testFunc = (string a, string b) => a + b;

            // act
            var success = value1
                .Map(testFunc)
                .Apply(value2);

            var result_message = success.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                success.IsSuccess.Should().BeTrue();
                result_message.Should().Be("input1input2");
            }
        }

        [Fact]
        public void First_fails()
        {
            // arrange
            var value1 = Result<string>.Failure("error");
            var value2 = Result<string>.Success("input2");
            var testFunc = (string a, string b) => a + b;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2);

            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }

        [Fact]
        public void Second_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Failure("error");
            var testFunc = (string a, string b) => a + b;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2);

            var result_message = failure.Match(d => d, err => err.Message);

            // assert
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }

        [Fact]
        public void Both_fail()
        {
            // arrange
            var value1 = Result<string>.Failure("error1");
            var value2 = Result<string>.Failure("error2");
            var testFunc = (string a, string b) => a + b;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("There are 2 Errors. ");
                error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2]");
            }
        }
    }

    public sealed class Apply_three_params
    {
        [Fact]
        public void Success()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Success("input3");
            var testFunc = (string a, string b, string c) => a + b + c;

            // act
            var success = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3);


            // assert
            var value = success.GetValue();
            using (new AssertionScope())
            {
                success.IsSuccess.Should().BeTrue();
                value.Should().Be("input1input2input3");
            }
        }

        [Fact]
        public void First_fails()
        {
            // arrange
            var value1 = Result<string>.Failure("error");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Success("input3");
            var testFunc = (string a, string b, string c) => a + b + c;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Second_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Failure("error");
            var value3 = Result<string>.Success("input3");
            var testFunc = (string a, string b, string c) => a + b + c;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Third_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Failure("error");
            var testFunc = (string a, string b, string c) => a + b + c;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void All_three_fail()
        {
            // arrange
            var value1 = Result<string>.Failure("error1");
            var value2 = Result<string>.Failure("error2");
            var value3 = Result<string>.Failure("error3");
            var testFunc = (string a, string b, string c) => a + b + c;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("There are 3 Errors. ");
                error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3]");
            }
        }
    }
    public sealed class Apply_four_params
    {
        [Fact]
        public void Success()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Success("input3");
            var value4 = Result<string>.Success("input4");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var success = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var value = success.GetValue();
            using (new AssertionScope())
            {
                success.IsSuccess.Should().BeTrue();
                value.Should().Be("input1input2input3input4");
            }
        }

        [Fact]
        public void First_fails()
        {
            // arrange
            var value1 = Result<string>.Failure("error");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Success("input3");
            var value4 = Result<string>.Success("input4");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Second_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Failure("error");
            var value3 = Result<string>.Success("input3");
            var value4 = Result<string>.Success("input4");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void Third_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Failure("error");
            var value4 = Result<string>.Success("input4");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }
        [Fact]
        public void Fourth_fails()
        {
            // arrange
            var value1 = Result<string>.Success("input1");
            var value2 = Result<string>.Success("input2");
            var value3 = Result<string>.Success("input3");
            var value4 = Result<string>.Failure("error");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("error");
            }
        }

        [Fact]
        public void All_four_fail()
        {
            // arrange
            var value1 = Result<string>.Failure("error1");
            var value2 = Result<string>.Failure("error2");
            var value3 = Result<string>.Failure("error3");
            var value4 = Result<string>.Failure("error4");
            var testFunc = (string a, string b, string c, string d) => a + b + c + d;

            // act
            var failure = value1
                .Map(testFunc)
                .Apply(value2)
                .Apply(value3)
                .Apply(value4);


            // assert
            var error = failure.GetError();
            using (new AssertionScope())
            {
                failure.IsSuccess.Should().BeFalse();
                error.Should().NotBeNull();
                error!.Message.Should().Be("There are 4 Errors. ");
                error!.Display.Should().Be("CompositeError: [TextError: error1, TextError: error2, TextError: error3, TextError: error4]");
            }
        }
    }
}