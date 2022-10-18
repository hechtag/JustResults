namespace Results.Errors;

public sealed class CompositeError : IError
{
    private CompositeError()
    {
        
    }
    public List<IError> Errors { get; init; } = new();
    public string Message => $"There are {Errors.Count} Errors. ";

    public static IError Create(IError first, IError second)
    {
        return new CompositeError { Errors = new List<IError> { first, second } };
    }
    
    public static IError Create(IEnumerable<IError> en)
    {
        return new CompositeError { Errors = en.ToList() };
    }
}