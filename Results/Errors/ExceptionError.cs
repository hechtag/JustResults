namespace Results.Errors;

public record ExceptionError(string Message) : IError
{
    public Exception? Exception { get; init; }

    public static implicit operator ExceptionError(Exception e) => new(e.Message) { Exception = e };

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
}