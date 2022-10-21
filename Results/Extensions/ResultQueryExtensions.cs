namespace Results;

public static class ResultQueryExtensions
{
    public static Result<TOutput> Select<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, TOutput> func)
        => input.Map(func);

    public static Result<TOutput> SelectMany<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, Result<TOutput>> func)
        => input.Bind(func);

    public static Result<TOutput> SelectMany<TInput1, TInput2, TOutput>(
        this Result<TInput1> input,
        Func<TInput1, Result<TInput2>> bindFunc,
        Func<TInput1, TInput2, TOutput> func)
    {
        return input.SelectMany(x => bindFunc(x).SelectMany(y => Result<TOutput>.Success(func(x, y))));
    }
}