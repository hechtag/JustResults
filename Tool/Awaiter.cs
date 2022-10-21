using System.Runtime.CompilerServices;

public class Awaiter<TResult> : INotifyCompletion
{
    private readonly Task<TResult> _input;

    public Awaiter(Task<TResult> input)
    {
        _input = input;
    }

    public bool IsCompleted => _input.IsCompleted;

    public TResult GetResult()
    {
        return _input.GetAwaiter().GetResult();
    }

    public void OnCompleted(Action continuation)
    {
        _input.ContinueWith(_ => continuation());
    }
}