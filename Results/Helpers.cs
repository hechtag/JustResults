namespace Results;

public static class Helpers
{
    public static async Task<TResult> MapTask<TInput, TResult>(this Task<TInput> input, Func<TInput, TResult> func)
        => func(await input);

    public static Task<T> ToTask<T>(this T input)
        => Task.FromResult(input);

    public static async Task<T> Flatten<T>(this Task<Task<T>> input)
        => await await input;
}