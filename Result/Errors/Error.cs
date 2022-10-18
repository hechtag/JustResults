namespace Result.Errors;

public record Error(string Message)
{
    public Exception? Exception { get; init; }

    public static implicit operator List<Error>(Error e) => new() { e };

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