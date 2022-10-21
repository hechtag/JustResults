using Results.Errors;

namespace Results.Asynchronous;

public sealed class AsyncResult<TSuccess>
{
    private AsyncResult(IError error) => Error = error;

    private readonly IError? Error;

    public bool IsSuccess => Error is null;

    private AsyncResult(Task<TSuccess> value) => _value = value;

    internal readonly Task<TSuccess>? _value;

    public static AsyncResult<TSuccess> Success(Task<TSuccess> success) =>
        new(success);

    public new static AsyncResult<TSuccess> Failure(IError error) =>
        new(error);

    public new static AsyncResult<TSuccess> Failure(string error) =>
        new(TextError.Create(error));

    public async Task<TSuccess>? GetValue()
        => IsSuccess
            ? await _value!
            : default(TSuccess?);


    public IError? GetError()
    {
        return IsSuccess
            ? null
            : Error;
    }

    public async Task<AsyncResult<TResult>> Map<TResult>(Func<TSuccess, TResult> mapFunc) =>
        IsSuccess
            ? AsyncResult<TResult>.Success(_value!.Map(mapFunc))
            : AsyncResult<TResult>.Failure(Error!);
}

internal static class asd
{
    internal static async Task<T> Map<T, S>(this Task<S> input, Func<S, T> func)
    {
        return func(await input);
    }
}