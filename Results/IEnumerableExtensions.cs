using Results.Errors;

namespace Results;

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

    public static Result<List<TSuccess>> SequenceApply<TSuccess>(this IEnumerable<Result<TSuccess>> input)
    {
        var start = Result<List<TSuccess>>.Success(new List<TSuccess>());
        return input.Aggregate(start, Map2);
    }

    private static Result<List<TSuccess>> Map2<TSuccess>(Result<List<TSuccess>> agg, Result<TSuccess> item)
    {
        List<TSuccess> AppendItem(List<TSuccess> l, TSuccess i) => l.Append(i).ToList();
        return Result<List<TSuccess>>.Map2(agg, item, AppendItem);
    }

    public static Result<List<TSuccess>> SequenceBind<TSuccess>(this IEnumerable<Result<TSuccess>> input)
    {
        var start = Result<List<TSuccess>>.Success(new List<TSuccess>());
        return input.Aggregate(start, Bind);
    }

    private static Result<List<TSuccess>> Bind<TSuccess>(Result<List<TSuccess>> agg, Result<TSuccess> item)
    {
        List<TSuccess> AppendItem(List<TSuccess> l, TSuccess i) => l.Append(i).ToList();
        return agg.Bind(a => item.Match(i => Result<List<TSuccess>>.Success(AppendItem(a, i)),
            e => Result<List<TSuccess>>.Failure(e)));
    }
}