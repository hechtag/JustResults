using Result.Errors;

namespace Result;

public sealed class Result<TSuccess> : Result
{
    private TSuccess _value = default!;

    public static Result<TSuccess> Success(TSuccess success) =>
        new() { _value = success };

    public new static Result<TSuccess> Failure(List<Error> errorList) => 
        new() { ErrorList = errorList };

    public TMatch Match<TMatch>(Func<TSuccess, TMatch> successFunc, Func<List<Error>, TMatch> errorFunc) =>
        IsSuccess
            ? successFunc(_value)
            : errorFunc(ErrorList);

    public Result<TResult> Map<TResult>(Func<TSuccess, TResult> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(mapFunc(_value))
            : Result<TResult>.Failure(ErrorList);

    public Result<TResult> Bind<TResult>(Func<TSuccess, Result<TResult>> bindFunc) =>
        IsSuccess
            ? bindFunc(_value)
            : Result<TResult>.Failure(ErrorList);

    public Result<TSuccess> Tap(Action<TSuccess> successTap, Action<List<Error>> failureTap)
    {
        if (IsSuccess)
            successTap(_value);
        else
            failureTap(ErrorList);

        return this;
    }

    public Result ToResult() =>
        IsSuccess
            ? Result.Success()
            : Result.Failure(ErrorList);

    public static implicit operator Result<TSuccess>(TSuccess s) => Success(s);
    public static implicit operator Task<Result<TSuccess>>(Result<TSuccess> s) => Task.FromResult(s);

    public static Result<TResult> Map2<TSuccess1, TSuccess2, TResult>(Result<TSuccess1> res1, Result<TSuccess2> res2,
        Func<TSuccess1, TSuccess2, TResult> mapFunc) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Result<TResult>.Success(mapFunc(res1._value, res2._value)),
            (false, false) => Result<TResult>.Failure(res1.ErrorList.Concat(res2.ErrorList).ToList()),
            (false, _) => Result<TResult>.Failure(res1.ErrorList),
            _ => Result<TResult>.Failure(res2.ErrorList)
        };
} 

public class Result
{
    protected List<Error> ErrorList = new();

    public bool IsSuccess => !ErrorList.Any();

    public static Task<Result> TaskSuccess => Task.FromResult(Success());

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(List<Error> errorList)
    {
        return new Result { ErrorList = errorList };
    }

    public Result<TResult> Map<TResult>(Func<TResult> mapFunc) =>
        IsSuccess
            ? Result<TResult>.Success(mapFunc())
            : (Result<TResult>)Failure(ErrorList);

    public Result<TResult> Bind<TResult>(Func<Result<TResult>> bindFunc) =>
        IsSuccess
            ? bindFunc()
            : (Result<TResult>)Failure(ErrorList);

    public TMatch Match<TMatch>(Func<TMatch> successFunc, Func<List<Error>, TMatch> errorFunc)
    {
        return IsSuccess
            ? successFunc()
            : errorFunc(ErrorList);
    }

    public static Result Map2(Result res1, Result res2) =>
        (res1.IsSuccess, res2.IsSuccess) switch
        {
            (true, true) => Success(),
            (false, false) => Failure(res1.ErrorList.Concat(res2.ErrorList).ToList()),
            (false, _) => res1,
            _ => res2
        };

    public static implicit operator Task<Result>(Result s) => Task.FromResult(s);
    
    public static Result<TResult> Try<TResult>(Func<TResult> func)
    {
        try
        {
            return Result<TResult>.Success(func());
        }
        catch (Exception ex)
        {
            return (Result<TResult>)Failure(ex.ToError());
        }
    }
    public Result Tap(Action successTap, Action<List<Error>> failureTap)
    {
        if (IsSuccess)
            successTap();
        else
            failureTap(ErrorList);

        return this;
    }
}