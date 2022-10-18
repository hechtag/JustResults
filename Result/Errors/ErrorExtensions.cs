namespace Result.Errors;

public static class ErrorExtensions
{
    public static Error ToError(this Exception ex) =>
        new(ex.Message) { Exception = ex };
}