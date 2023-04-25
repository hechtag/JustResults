using Hechtag.JustResults;

namespace Hechtag.JustResults;

public static class ResultBindExtensions
{
    public static async Task<Result<TResult>> BindAsync<TInput, TResult>(
        this Task<Result<TInput>> input,
        Func<TInput, Task<Result<TResult>>> bindFunc) where TResult : notnull where TInput : notnull
        => await input.MapTask(i => i.Bind(bindFunc)).FlattenTask();
}