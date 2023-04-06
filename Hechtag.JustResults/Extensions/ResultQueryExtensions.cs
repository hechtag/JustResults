using Hechtag.JustResults;

namespace Hechtag.JustResults;

public static class ResultQueryExtensions
{
    public static Result<TOutput> Select<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, TOutput> func)
        => input.Map(func);

    public static Task<Result<TOutput>> Select<TInput, TOutput>(
        this Task<Result<TInput>> input,
        Func<TInput, TOutput> func)
        => input.MapTask(i => i.Select(func));

    public static Result<TOutput> SelectMany<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, Result<TOutput>> func)
        => input.Bind(func);
    //
    // public static Task<Result<TOutput>> SelectMany<TInput, TOutput>(
    //     this Task<Result<TInput>> input,
    //     Func<TInput, Result<TOutput>> func)
    //     => input.MapTask(i => i.SelectMany(func));

    public static Result<TOutput> SelectMany<TInput1, TInput2, TOutput>(
        this Result<TInput1> input,
        Func<TInput1, Result<TInput2>> bindFunc,
        Func<TInput1, TInput2, TOutput> func)
        => input.SelectMany(i1 => bindFunc(i1).Select(i2 => func(i1, i2)));

    public static Task<Result<TOutput>> SelectMany<TInput1, TInput2, TOutput>(
        this Task<Result<TInput1>> input,
        Func<TInput1, Task<Result<TInput2>>> bindFunc,
        Func<TInput1, TInput2, TOutput> func)
    {
        Task<Result<TOutput>> OuterBindFunc(TInput1 i1) => bindFunc(i1).Select(i2 => func(i1, i2));
        return input.BindAsync(OuterBindFunc);
    }

    public static Task<Result<TOutput>> SelectMany<TInput1, TInput2, TOutput>(
        this Result<TInput1> input,
        Func<TInput1, Task<Result<TInput2>>> bindFunc,
        Func<TInput1, TInput2, TOutput> func)
    {
        Task<Result<TOutput>> OuterBindFunc(TInput1 i1) => bindFunc(i1).Select(i2 => func(i1, i2));
        return input.Bind(OuterBindFunc);
    }

    public static Task<Result<TOutput>> SelectMany<TInput1, TInput2, TOutput>(
        this Task<Result<TInput1>> input,
        Func<TInput1, Result<TInput2>> bindFunc,
        Func<TInput1, TInput2, TOutput> func)
        => input.MapTask(i => i.SelectMany(bindFunc, func));
    
}