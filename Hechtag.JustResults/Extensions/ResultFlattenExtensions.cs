using Hechtag.JustResults;

namespace Hechtag.JustResults;

public static class ResultFlattenExtensions
{
    public static Result<TSuccess> Flatten<TSuccess>(this Result<Result<TSuccess>> input)
        => input.Match(s=> s, Result<TSuccess>.Failure);
    
    public static Result Flatten(this Result<Result> input)
        => input.Match(s=> s, Result.Failure);

    public static Task<Result<TSuccess>> FlattenAsync<TSuccess>(this Task<Result<Result<TSuccess>>> input)
        => input.MapTask(i => i.Flatten());
    
    
    public static Task<Result> FlattenAsync(this Task<Result<Result>> input)
        => input.MapTask(i => i.Flatten());

}