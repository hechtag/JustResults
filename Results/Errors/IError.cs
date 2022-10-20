namespace Results.Errors;

public interface IError
{
    public string Message { get; }

    public string Display { get; }
}