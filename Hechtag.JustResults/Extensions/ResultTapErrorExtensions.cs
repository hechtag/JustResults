using Hechtag.JustResults;
using Hechtag.JustResults.Errors;

namespace Hechtag.JustResults;

public static class ResultTapErrorExtensions
{
    public static Task<Result<TSuccess>> TapErrorAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Action<IError> failureTap)
        => input.MapTask(i => i.TapError(failureTap));

    public static Task<Result<TSuccess>> TapErrorAsync<TSuccess>(
        this Task<Result<TSuccess>> input,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.TapError(failureTap)).FlattenTask();

    public static Task<Result> TapErrorAsync(
        this Task<Result> input,
        Action<IError> failureTap)
        => input.MapTask(i => i.TapError(failureTap));

    public static Task<Result> TapErrorAsync(
        this Task<Result> input,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.TapError(failureTap)).FlattenTask();
}