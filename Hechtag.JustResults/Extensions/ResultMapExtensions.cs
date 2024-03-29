using Hechtag.JustResults;

namespace Hechtag.JustResults;

public static class ResultMapExtensions
{
    public static Task<Result<TOutput>> MapAsync<TInput, TOutput>(
        this Task<Result<TInput>> input,
        Func<TInput, TOutput> func) where TInput : notnull where TOutput : notnull
        => input.MapTask(res => res.Map(func));

    public static Task<Result<TOutput>> MapAsync<TInput, TOutput>(
        this Task<Result<TInput>> input,
        Func<TInput, Task<TOutput>> func) where TInput : notnull where TOutput : notnull
        => input.MapTask(res => res.Map(func)).FlattenTask();

    public static Task<Result<TOutput>> Map<TOutput>(
        this Task<Result> input,
        Func<TOutput> func) where TOutput : notnull
        => input.MapTask(res => res.Map(func));

    public static Task<Result<TOutput>> MapAsync<TOutput>(
        this Task<Result> input,
        Func<Task<TOutput>> func) where TOutput : notnull
        => input.MapTask(res => res.Map(func)).FlattenTask();

}