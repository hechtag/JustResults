namespace Results;

public static class TaskExtensions
{
    public static async Task<TResult> MapTask<TInput, TResult>(this Task<TInput> input, Func<TInput, TResult> func)
        => func(await input);

    public static Task<T> ToTask<T>(this T input)
        => Task.FromResult(input);

    public static async Task<T> FlattenTask<T>(this Task<Task<T>> input)
        => await await input;
}