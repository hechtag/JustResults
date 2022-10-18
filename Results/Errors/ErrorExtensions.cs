namespace Results.Errors;

public static class ErrorExtensions
{
    public static IError ToError(this Exception ex) =>
        new ExceptionError(ex.Message) { Exception = ex };

    public static IError Concat(this IError first, IError second)
    {
        if (first is CompositeError f1 && second is CompositeError s1)
        {
            return CompositeError.Create(f1.Errors.Concat(s1.Errors));
        }

        if (first is CompositeError f2)
        {
            return CompositeError.Create(f2.Errors.Append(second));
        }

        if (second is CompositeError s2)
        {
            return CompositeError.Create(s2.Errors.Prepend(first));
        }

        return CompositeError.Create(first, second);
    }
}