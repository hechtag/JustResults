using Results.Errors;

namespace Results.Synchronous;

public static class IEnumerableExtensions
{
    public static IEnumerable<TSuccess> WhereSuccess<TSuccess>(this IEnumerable<Result<TSuccess>> input)
        => input
            .Where(r => r.IsSuccess)
            .Select(r => r.GetValue()!);

    public static IEnumerable<IError> WhereFailure<TSuccess>(this IEnumerable<Result<TSuccess>> input)
        => input
            .Where(r => !r.IsSuccess)
            .Select(r => r.GetError()!);

    public static Result<IEnumerable<TSuccess>> SequenceApply<TSuccess>(this IEnumerable<Result<TSuccess>> input)
    {
        var start = Result<IEnumerable<TSuccess>>.Success(new List<TSuccess>());
        return input.Aggregate(start, Map2);
    }

    private static Result<IEnumerable<TSuccess>> Map2<TSuccess>(Result<IEnumerable<TSuccess>> agg,
        Result<TSuccess> item)
    {
        IEnumerable<TSuccess> AppendItem(IEnumerable<TSuccess> l, TSuccess i) => l.Append(i);
        return Result<List<TSuccess>>.Map2(agg, item, AppendItem);
    }

    public static Result<IEnumerable<TSuccess>> SequenceBind<TSuccess>(this IEnumerable<Result<TSuccess>> input)
    {
        var start = Result<IEnumerable<TSuccess>>.Success(new List<TSuccess>());
        return input.Aggregate(start, Bind);
    }

    private static Result<IEnumerable<TSuccess>> Bind<TSuccess>(Result<IEnumerable<TSuccess>> agg,
        Result<TSuccess> item)
    {
        IEnumerable<TSuccess> AppendItem(IEnumerable<TSuccess> l, TSuccess i) => l.Append(i);
        return agg.Bind(a => item.Map(i => AppendItem(a, i)));
    }
    
    
    public static Result<IEnumerable<TOutput>> TraverseApply<TInput,TOutput>(this IEnumerable<TInput> input, Func<TInput,Result<TOutput>> func)
    {
        var start = Result<IEnumerable<TOutput>>.Success(new List<TOutput>());
        return input.Aggregate(start, (agg, item) => Map2(agg,func(item)));
    }

    public static Result<IEnumerable<TOutput>> TraverseBind<TInput,TOutput>(this IEnumerable<TInput> input, Func<TInput,Result<TOutput>> func)
    {
        var start = Result<IEnumerable<TOutput>>.Success(new List<TOutput>());
        return input.Aggregate(start, (agg, item) => Bind(agg,func(item)));
    }

}