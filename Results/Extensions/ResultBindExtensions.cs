namespace Results;

public static class ResultBindExtensions
{
    public static async Task<Result<TResult>> BindAsync<TInput, TResult>(
        this Task<Result<TInput>> input,
        Func<TInput, Task<Result<TResult>>> bindFunc)
        => await input.MapTask(i => i.Bind(bindFunc)).FlattenTask();
}