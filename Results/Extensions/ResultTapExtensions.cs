using Results.Errors;

namespace Results;

public static class ResultTapExtensions
{
    public static Task<Result<TSuccess>> Tap<TSuccess>(
        this Task<Result<TSuccess>> input,
        Action<TSuccess> successTap,
        Action<IError> failureTap)
        => input.MapTask(i => i.Tap(successTap, failureTap));

    public static Task<Result<TSuccess>> Tap<TSuccess>(
        this Task<Result<TSuccess>> input,
        Func<TSuccess, Task> successTap,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.Tap(successTap, failureTap)).FlattenTask();
    
    public static Task<Result> Tap(
        this Task<Result> input,
        Action successTap,
        Action<IError> failureTap)
        => input.MapTask(i => i.Tap(successTap, failureTap));

    public static Task<Result> Tap(
        this Task<Result> input,
        Func<Task> successTap,
        Func<IError, Task> failureTap)
        => input.MapTask(i => i.Tap(successTap, failureTap)).FlattenTask();
}