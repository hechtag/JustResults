namespace Hechtag.JustResults.Errors;

public sealed class TextError : IError
{
    private TextError(string text)
    {
        Message = text;
    }

    public static implicit operator TextError(string text) => new(text);

    public string Message { get; }
    public string Display => $"{nameof(TextError)}: {Message}";

    public static IError Create(string text)
    {
        return new TextError(text);
    }

    public override string ToString()
    {
        return Display;
    }
}