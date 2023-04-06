namespace Hechtag.JustResults.Option;

public sealed class Option<TSuccess>
{
    // TODO: TESTS!!
    private Option() { }
    private Option(TSuccess value) => _value = value;
    public bool IsSuccess => _value is not null;

    private readonly TSuccess? _value;

    public static Option<TSuccess> Success(TSuccess success) =>
        new(success);

    public static async Task<Option<TSuccess>> Success(Task<TSuccess> success) =>
        new(await success);

    public new static Option<TSuccess> Failure() => new();


    public TMatch Match<TMatch>(Func<TSuccess, TMatch> successFunc, Func<TMatch> errorFunc) =>
        IsSuccess
            ? successFunc(_value!)
            : errorFunc();

    public Option<TResult> Map<TResult>(Func<TSuccess, TResult> mapFunc) =>
        IsSuccess
            ? Option<TResult>.Success(mapFunc(_value!))
            : Option<TResult>.Failure();

    public async Task<Option<TResult>> Map<TResult>(Func<TSuccess, Task<TResult>> mapFunc) =>
        IsSuccess
            ? Option<TResult>.Success(await mapFunc(_value!))
            : Option<TResult>.Failure();

    public Option<TResult> Bind<TResult>(Func<TSuccess, Option<TResult>> bindFunc) =>
        IsSuccess
            ? bindFunc(_value!)
            : Option<TResult>.Failure();

    public async Task<Option<TResult>> Bind<TResult>(Func<TSuccess, Task<Option<TResult>>> bindFunc) =>
        IsSuccess
            ? await bindFunc(_value!)
            : Option<TResult>.Failure();

    public Option<TSuccess> TapBoth(Action<TSuccess> successTap, Action failureTap)
    {
        if (IsSuccess)
            successTap(_value!);
        else
            failureTap();

        return this;
    }

    public async Task<Option<TSuccess>> TapBoth(Func<TSuccess, Task> successTap, Func<Task> failureTap)
    {
        if (IsSuccess)
            await successTap(_value!);
        else
            await failureTap();

        return this;
    }

    public Option<TSuccess> TapError(Action failureTap)
    {
        if (!IsSuccess)
            failureTap();

        return this;
    }

    public async Task<Option<TSuccess>> TapError(Func<Task> failureTap)
    {
        if (!IsSuccess)
            await failureTap();

        return this;
    }

    public Option<TSuccess> Tap(Action<TSuccess> successTap)
    {
        if (IsSuccess)
            successTap(_value!);

        return this;
    }

    public async Task<Option<TSuccess>> Tap(Func<TSuccess, Task> successTap)
    {
        if (IsSuccess)
            await successTap(_value!);

        return this;
    }

    public TSuccess? GetValue()
        => IsSuccess
            ? _value
            : default;

    public static implicit operator Option<TSuccess>(TSuccess s) => Success(s);

    public static Option<TSuccess> Map2<TSuccess1, TSuccess2>(
        Option<TSuccess1> res1,
        Option<TSuccess2> res2,
        Func<TSuccess1, TSuccess2, TSuccess> mapFunc) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(mapFunc(res1._value!, res2._value!)),
            _ => Failure()
        };

    public static async Task<Option<TSuccess>> Map2<TSuccess1, TSuccess2>(
        Option<TSuccess1> res1,
        Option<TSuccess2> res2,
        Func<TSuccess1, TSuccess2, Task<TSuccess>> mapFunc) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(await mapFunc(res1._value!, res2._value!)),
            _ => Failure()
        };

    public static async Task<Option<TSuccess>> Map2Async<TSuccess1, TSuccess2>(
        Task<Option<TSuccess1>> res1,
        Task<Option<TSuccess2>> res2,
        Func<TSuccess1, TSuccess2, Task<TSuccess>> mapFunc)
        => await Map2(await res1, await res2, mapFunc);

    public override string ToString()
    {
        return IsSuccess
            ? $"Option: Success ({_value.ToString()})"
            : $"Option: Failure";
    }
}
