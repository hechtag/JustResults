using Hechtag.JustResults;
using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults;

public static class FromBoolExtensions
{
    public static Result FromBool(this bool input, IError error)
    {
        return input
        ? Result.Success()
        : Result.Failure(error);
    }

    public static Result FromBool(this bool input, string error)
    {
        return input.FromBool(TextError.Create(error));
    }
    
    public static async Task<Result> FromBool(this Task<bool> input, IError error)
    {
        return (await input).FromBool(error);
    }
    
    public static async Task<Result> FromBool(this Task<bool> input, string error)
    {
        return (await input).FromBool(error);
    }
}