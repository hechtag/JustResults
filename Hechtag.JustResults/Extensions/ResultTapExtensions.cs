using Hechtag.JustResults;
using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults;

public static class ResultTapExtensions
{
    public static Task<Result<TSuccess>> TapAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Action<TSuccess> successTap,
        Action<IError> failureTap)
        => input.MapTask(i => i.TapBoth(successTap, failureTap));

    public static Task<Result<TSuccess>> TapAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Func<TSuccess, Task> successTap,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.TapBoth(successTap, failureTap)).FlattenTask();

    public static Task<Result> TapAsync(
        this Task<Result> input,
        Action successTap,
        Action<IError> failureTap)
        => input.MapTask(i => i.TapBoth(successTap, failureTap));

    public static Task<Result> TapAsync(
        this Task<Result> input,
        Func<Task> successTap,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.TapBoth(successTap, failureTap)).FlattenTask();
}