namespace Results.Synchronous;

public static class ResultMapExtensions
{
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