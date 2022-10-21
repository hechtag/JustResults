using Results.Errors;

namespace Results.Synchronous;

public static class ResultExtensions
{
    public static Result<TInput> FailIfNull<TInput>(this TInput? input, IError error)
        => input is not null
            ? Result<TInput>.Success(input)
            : Result<TInput>.Failure(error);

    public static Result<TInput> FailIfNull<TInput>(this TInput? input, string errorMessage = "The input was null. ")
        => input.FailIfNull(TextError.Create(errorMessage));
}