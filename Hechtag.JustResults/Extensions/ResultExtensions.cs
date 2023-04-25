using Hechtag.JustResults;
using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults;

public static class ResultExtensions
{
    public static Result<TInput> FailIfNull<TInput>(this TInput? input, IError error) where TInput : notnull
        => input is not null
            ? Result<TInput>.Success(input)
            : Result<TInput>.Failure(error);

    public static Result<TInput> FailIfNull<TInput>(this TInput? input, string errorMessage = "The input was null. ") where TInput : notnull
        => input.FailIfNull(TextError.Create(errorMessage));

    public static Task<Result<TInput>> FailIfNullAsync<TInput>(this Task<TInput?> input,
        string errorMessage = "The input was null. ") where TInput : notnull
        => input.MapTask(i => i.FailIfNull(TextError.Create(errorMessage)));

    public static Task<Result> ToResultAsync<TSuccess>(this Task<Result<TSuccess>> input) where TSuccess : notnull
        => input.MapTask(i => i.ToResult());
}