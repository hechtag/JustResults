using System.Runtime.CompilerServices;

[AsyncMethodBuilder(typeof(AwaitableMethodBuilder<>))]
public sealed class Awaitable<TResult>
{
    private readonly Task<TResult> _input;

    public Awaitable(Task<TResult> input)
    {
        _input = input;
    }

    // public async Awaitable Map(Awaitable input, Func<string, string> func)
    // {
    //     var asd = await input;
    //     return func(asd);
    // }

    public Awaiter<TResult> GetAwaiter()
    {
        return new Awaiter<TResult>(_input);
    }
}