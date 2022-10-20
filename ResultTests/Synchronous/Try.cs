using Results.Synchronous;

namespace ResultTests.Synchronous;

public class Try
{
     public sealed class With_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var func = () => "try";
        
            // act
            var res = Result<string>.Try(func);
        
            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeTrue();
                result_message.Should().Be("try");
            }
        }
        
        [Fact]
        public void Failure()
        {
            // arrange
            var func = new Func<string>(() => throw new Exception("error"));
        
            // act
            var res = Result<string>.Try(func);
        
            // assert
            var result_message = res.Match(data => data, err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }
    }  
    
    public sealed class No_Content
    {
        [Fact]
        public void Success()
        {
            // arrange
            var func = () => { };
        
            // act
            var res = Results.Synchronous.Result.Try(func);
        
            // assert
            var result_message = res.Match(() => "try", err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeTrue();
                result_message.Should().Be("try");
            }
        }
        
        [Fact]
        public void Failure()
        {
            // arrange
            var func = new Action(() => throw new Exception("error"));
        
            // act
            var res = Results.Synchronous.Result.Try(func);
        
            // assert
            var result_message = res.Match(() => "try", err => err.Message);
            using (new AssertionScope())
            {
                res.IsSuccess.Should().BeFalse();
                result_message.Should().Be("error");
            }
        }
    }
}