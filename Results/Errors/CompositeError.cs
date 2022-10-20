namespace Results.Errors;

public sealed class CompositeError : IError
{
    private CompositeError(List<IError> errors)
    {
        Errors = errors;
        Display = $"{nameof(CompositeError)}: [{string.Join(", ", Errors.Select(e => e.Display))}]";
    }

    public List<IError> Errors { get; }
    public string Message => $"There are {Errors.Count} Errors. ";
    public string Display { get; }

    public static IError Create(IError first, IError second)
    {
        return new CompositeError(new List<IError> { first, second });
    }

    public static IError Create(IEnumerable<IError> en)
    {
        return new CompositeError(en.ToList());
    }
    
    public override string ToString()
    {
        return Display;
    }
}