using Hechtag.JustResults.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hechtag.JustResults.Extensions
{
    public static class CreationExtensions
    {
        public static Result<TSuccess> ToSuccess<TSuccess>(this TSuccess input) where TSuccess : notnull
        {
            return Result<TSuccess>.Success(input);
        }

        public static async Task<Result<TSuccess>> ToSuccess<TSuccess>(this Task<TSuccess> input) where TSuccess : notnull
        {
            return (await input).ToSuccess();
        }

        public static Result<TSuccess> ToFailure<TSuccess>(this IError error) where TSuccess : notnull
        {
            return Result<TSuccess>.Failure(error);
        }

        public static async Task<Result<TSuccess>> ToFailure<TSuccess>(this Task<IError> error) where TSuccess : notnull
        {
            return (await error).ToFailure<TSuccess>();
        }

        public static Result<TSuccess> ToFailure<TSuccess>(this string error) where TSuccess : notnull
        {
            return TextError.Create(error).ToFailure<TSuccess>();
        }

        public static async Task<Result<TSuccess>> ToFailure<TSuccess>(this Task<string> error) where TSuccess : notnull    
        {
            return (await error).ToFailure<TSuccess>();
        }

    }
}
