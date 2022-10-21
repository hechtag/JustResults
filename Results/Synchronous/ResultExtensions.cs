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

    public static Task<Result<TOutput>> Map<TInput, TOutput>(
        this Task<Result<TInput>> input,
        Func<TInput, TOutput> func)
        => input.MapTask(res => res.Map(func));

    public static Task<Result<TOutput>> Map<TInput, TOutput>(
        this Task<Result<TInput>> input,
        Func<TInput, Task<TOutput>> func)
        => input.MapTask(res => res.Map(func)).Flatten();
    
    public static Task<Result<TOutput>> Map<TOutput>(
        this Task<Result> input,
        Func<TOutput> func)
        => input.MapTask(res => res.Map(func));

    public static Task<Result<TOutput>> Map<TOutput>(
        this Task<Result> input,
        Func<Task<TOutput>> func)
        => input.MapTask(res => res.Map(func)).Flatten();
}