namespace Results.Errors;

public sealed class TextError : IError
{
    private TextError(string text)
    {
        Message = text;
    }

    public static implicit operator TextError(string text) => new TextError(text);
    
    public string Message { get; }

    public static IError Create(string text)
    {
        return new TextError(text);
    }
}