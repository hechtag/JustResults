using Hechtag.JustResults.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hechtag.JustResults.Option
{
    public static class TransformExtensions
    {
        // TODO: TESTS!!
        public static Option<TSuccess> ToOption<TSuccess>(this Result<TSuccess> input)
        {
            return input.Match(
                i => Option<TSuccess>.Success(i),
                _ => Option<TSuccess>.Failure());
        }

        public static Result<TSuccess> ToResult<TSuccess>(this Option<TSuccess> input, IError error)
        {
            return input.Match(
                i => Result<TSuccess>.Success(i),
                () => Result<TSuccess>.Failure(error));
        }
        public static Result<TSuccess> ToResult<TSuccess>(this Option<TSuccess> input, string error)
        {
            return input.ToResult(TextError.Create(error));
        }
    }
}
