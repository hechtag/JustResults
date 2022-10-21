using System.Runtime.CompilerServices;

public struct AwaitableMethodBuilder<TResult>
{
    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine => stateMachine.MoveNext();

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }

    public void SetResult(TResult result)
    {
    }

    public void SetException(Exception exception)
    {
    }

    public Awaitable<TResult> Task { get; }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter,
        ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter,
        ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
    }

    public static AwaitableMethodBuilder<TResult> Create() => new();
}