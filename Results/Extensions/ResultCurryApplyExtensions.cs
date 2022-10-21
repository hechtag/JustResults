using Results.Errors;

namespace Results;

public static class ResultCurryApplyExtensions
{
    // public static Result<TOutput>
    //     Apply<TInput, TOutput>(this Result<Func<TInput, TOutput>> func, Result<TInput> input) =>
    //     func.Match(
    //         input.Map,
    //         eFunc => ApplyErrorPath<TInput, TOutput>(input, eFunc));

    public static Result<TOutput>
        Apply<TInput, TOutput>(this Result<Func<TInput, TOutput>> func, Result<TInput> input) =>
        Apply(func, input, (i, f) => f(i));

    // public static Result<Func<TInput2, TOutput>>
    //     Apply<TInput1, TInput2, TOutput>(
    //         this Result<Func<TInput1, TInput2, TOutput>> func, Result<TInput1> input) =>
    //     func.Match(
    //         func1 => input.Map<Func<TInput2, TOutput>>(i => j => func1(i, j)),
    //         eFunc => ApplyErrorPath<TInput1, Func<TInput2, TOutput>>(input, eFunc)
    //     );

    public static Result<Func<TInput2, TOutput>>
        Apply<TInput1, TInput2, TOutput>(
            this Result<Func<TInput1, TInput2, TOutput>> func, Result<TInput1> input) =>
        Apply<TInput1, Func<TInput1, TInput2, TOutput>, Func<TInput2, TOutput>>(func, input,
            (i1, f) => i2 => f(i1, i2));

    public static Result<Func<TInput2, TInput3, TOutput>>
        Apply<TInput1, TInput2, TInput3, TOutput>(
            this Result<Func<TInput1, TInput2, TInput3, TOutput>> func, Result<TInput1> input) =>
        Apply<TInput1, Func<TInput1, TInput2, TInput3, TOutput>, Func<TInput2, TInput3, TOutput>>(func, input,
            (i1, f) => (i2, i3) => f(i1, i2, i3));

    private static Result<TFuncOutput> Apply<TInput, TFuncInput, TFuncOutput>(this Result<TFuncInput> func,
        Result<TInput> input, Func<TInput, TFuncInput, TFuncOutput> metaFunc) =>
        func.Match(s => input.Map(i => metaFunc(i, s)),
            eFunc => ApplyErrorPath<TInput, TFuncOutput>(input, eFunc));

    private static Result<TOutput> ApplyErrorPath<TInput, TOutput>(Result<TInput> input, IError eFunc) =>
        input.Match(
            _ => Result<TOutput>.Failure(eFunc),
            eInput => Result<TOutput>.Failure(eFunc.Concat(eInput))
        );
}