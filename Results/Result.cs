using Results.Errors;

namespace Results;

public sealed class Result<TSuccess> : Result
{
    private Result(TSuccess value) : base(null) => _value = value;

    private Result(IError error) : base(error)
    {
    }

    private readonly TSuccess? _value;

    public static Result<TSuccess> Success(TSuccess success) =>
        new(success);

    public static async Task<Result<TSuccess>> Success(Task<TSuccess> success) =>
        new(await success);

    public new static Result<TSuccess> Failure(IError error) =>
        new(error);

    public new static Result<TSuccess> Failure(string error) =>
        new(TextError.Create(error));

    public TMatch Match<TMatch>(Func<TSuccess, TMatch> successFunc, Func<IError, TMatch> errorFunc) =>
        IsSuccess
            ? successFunc(_value!)
            : errorFunc(Error!);

    public Result<TResult> Map<TResult>(Func<TSuccess, TResult> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(mapFunc(_value!))
            : Result<TResult>.Failure(Error!);

    public async Task<Result<TResult>> Map<TResult>(Func<TSuccess, Task<TResult>> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(await mapFunc(_value!))
            : Result<TResult>.Failure(Error!);

    public Result<TResult> Bind<TResult>(Func<TSuccess, Result<TResult>> bindFunc) =>
        IsSuccess
            ? bindFunc(_value!)
            : Result<TResult>.Failure(Error!);

    public async Task<Result<TResult>> Bind<TResult>(Func<TSuccess, Task<Result<TResult>>> bindFunc) =>
        IsSuccess
            ? await bindFunc(_value!)
            : Result<TResult>.Failure(Error!);

    public Result<TSuccess> Tap(Action<TSuccess> successTap, Action<IError> failureTap)
    {
        if (IsSuccess)
            successTap(_value!);
        else
            failureTap(Error!);

        return this;
    }

    public async Task<Result<TSuccess>> Tap(Func<TSuccess, Task> successTap, Func<IError, Task> failureTap)
    {
        if (IsSuccess)
            await successTap(_value!);
        else
            await failureTap(Error!);

        return this;
    }

    public TSuccess? GetValue()
        => IsSuccess
            ? _value
            : default(TSuccess?);

    public Result ToResult() =>
        IsSuccess
            ? Result.Success()
            : Result.Failure(Error!);

    public static implicit operator Result<TSuccess>(TSuccess s) => Success(s);
    public static implicit operator Task<Result<TSuccess>>(Result<TSuccess> s) => Task.FromResult(s);

    public static Result<TSuccess> Map2<TSuccess1, TSuccess2>(
        Result<TSuccess1> res1,
        Result<TSuccess2> res2,
        Func<TSuccess1, TSuccess2, TSuccess> mapFunc) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(mapFunc(res1._value!, res2._value!)),
            (false, false) => Failure(res1.Error!.Concat(res2.Error!)),
            (false, _) => Failure(res1.Error!),
            _ => Failure(res2.Error!)
        };

    public static async Task<Result<TSuccess>> Map2<TSuccess1, TSuccess2>(
        Result<TSuccess1> res1,
        Result<TSuccess2> res2,
        Func<TSuccess1, TSuccess2, Task<TSuccess>> mapFunc) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(await mapFunc(res1._value!, res2._value!)),
            (false, false) => Failure(res1.Error!.Concat(res2.Error!)),
            (false, _) => Failure(res1.Error!),
            _ => Failure(res2.Error!)
        };

    public static async Task<Result<TSuccess>> Map2<TSuccess1, TSuccess2>(
        Task<Result<TSuccess1>> res1,
        Task<Result<TSuccess2>> res2,
        Func<TSuccess1, TSuccess2, Task<TSuccess>> mapFunc)
        => await Map2(await res1, await res2, mapFunc);

    public static Result<TResult> Try<TResult>(Func<TResult> func)
    {
        try
        {
            return Result<TResult>.Success(func());
        }
        catch (Exception ex)
        {
            return Result<TResult>.Failure(ex.ToError());
        }
    }

    public static async Task<Result<TResult>> Try<TResult>(Func<Task<TResult>> func)
    {
        try
        {
            return Result<TResult>.Success(await func());
        }
        catch (Exception ex)
        {
            return Result<TResult>.Failure(ex.ToError());
        }
    }
}

public class Result
{
    protected Result(IError error) => Error = error;

    protected readonly IError? Error;

    public bool IsSuccess => Error is null;

    public static Task<Result> TaskSuccess => Task.FromResult(Success());

    public static Result Success()
    {
        return new Result(null);
    }

    public static Result Failure(IError error)
    {
        return new Result(error);
    }

    public static Result Failure(string error)
    {
        return new Result(TextError.Create(error));
    }

    public Result<TResult> Map<TResult>(Func<TResult> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(mapFunc())
            : Result<TResult>.Failure(Error!);

    public async Task<Result<TResult>> Map<TResult>(Func<Task<TResult>> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(await mapFunc())
            : Result<TResult>.Failure(Error!);

    public Result<TResult> Bind<TResult>(Func<Result<TResult>> bindFunc) =>
        IsSuccess
            ? bindFunc()
            : Result<TResult>.Failure(Error!);

    public async Task<Result<TResult>> Bind<TResult>(Func<Task<Result<TResult>>> bindFunc) =>
        IsSuccess
            ? await bindFunc()
            : Result<TResult>.Failure(Error!);

    public TMatch Match<TMatch>(Func<TMatch> successFunc, Func<IError, TMatch> errorFunc)
    {
        return IsSuccess
            ? successFunc()
            : errorFunc(Error!);
    }

    public static Result Map2(Result res1, Result res2) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(),
            (false, false) => Failure(res1.Error!.Concat(res2.Error!)),
            (false, _) => res1,
            _ => res2
        };

    public static async Task<Result> Map2(Task<Result> res1Task, Task<Result> res2Task)
    {
        var (res1, res2) = (await res1Task, await res2Task);
        return (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(),
            (false, false) => Failure(res1.Error!.Concat(res2.Error!)),
            (false, _) => res1,
            _ => res2
        };
    }

    public static Result Try(Action func)
    {
        try
        {
            func();
            return Success();
        }
        catch (Exception ex)
        {
            return Failure(ex.ToError());
        }
    }

    public static async Task<Result> Try(Func<Task> func)
    {
        try
        {
            await func();
            return Success();
        }
        catch (Exception ex)
        {
            return Failure(ex.ToError());
        }
    }

    public static implicit operator Task<Result>(Result s) => Task.FromResult(s);

    public Result Tap(Action successTap, Action<IError> failureTap)
    {
        if (IsSuccess)
            successTap();
        else
            failureTap(Error!);

        return this;
    }

    public async Task<Result> Tap(Func<Task> successTap, Func<IError, Task> failureTap)
    {
        if (IsSuccess)
            await successTap();
        else
            await failureTap(Error!);

        return this;
    }

    public IError? GetError()
    {
        return IsSuccess
            ? null
            : Error;
    }
}