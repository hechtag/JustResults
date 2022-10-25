using Hechtag.JustResults;

namespace Hechtag.JustResults.Errors;

public sealed class ExceptionError : IError
{
    public ExceptionError(Exception ex)
    {
        Exception = ex;
    }

    public Exception Exception { get; }
    public string Message => Exception.Message;
    public string Display => $"{nameof(ExceptionError)}: {Exception}";

    public static implicit operator ExceptionError(Exception e) => new(e);

    public static Result Try(Action action)
    {
        try
        {
            action();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.ToError());
        }
    }
    public override string ToString()
    {
        return Display;
    }

}