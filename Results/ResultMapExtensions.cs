namespace Results;

public static class ResultMapExtensions
{
    public static Result<Func<TInput2, TOutput>> Map<TInput1, TInput2, TOutput>(this Result<TInput1> input,
        Func<TInput1, TInput2, TOutput> mapFunc) =>
        input.Match(i1 => Result<Func<TInput2, TOutput>>.Success(i2 => mapFunc(i1, i2)),
           Result<Func<TInput2, TOutput>>.Failure);

    public static Result<Func<TInput2, TInput3, TOutput>> Map<TInput1, TInput2, TInput3, TOutput>(
        this Result<TInput1> input,
        Func<TInput1, TInput2, TInput3, TOutput> mapFunc) =>
        input.Match(i1 => Result<Func<TInput2, TInput3, TOutput>>.Success((i2, i3) => mapFunc(i1, i2, i3)),
           Result<Func<TInput2, TInput3, TOutput>>.Failure);
    
    public static Result<Func<TInput2, TInput3, TInput4, TOutput>> Map<TInput1, TInput2, TInput3, TInput4, TOutput>(
        this Result<TInput1> input,
        Func<TInput1, TInput2, TInput3, TInput4, TOutput> mapFunc) =>
        input.Match(i1 => Result<Func<TInput2, TInput3, TInput4, TOutput>>.Success((i2, i3, i4) => mapFunc(i1, i2, i3, i4)),
           Result<Func<TInput2, TInput3, TInput4, TOutput>>.Failure);
}