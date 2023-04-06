using Hechtag.JustResults;
using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults;

public static class ResultTapExtensions
{
    public static Task<Result<TSuccess>> TapAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Action<TSuccess> successTap)
        => input.MapTask(i => i.Tap(successTap));

    public static Task<Result<TSuccess>> TapAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Func<TSuccess, Task> successTap)
        => input.MapTask(i => i.Tap(successTap)).FlattenTask();

    public static Task<Result> TapAsync(
        this Task<Result> input,
        Action successTap)
        => input.MapTask(i => i.Tap(successTap));

    public static Task<Result> TapAsync(
        this Task<Result> input,
        Func<Task> successTap)
        => input.MapTask(i => i.Tap(successTap)).FlattenTask();
}