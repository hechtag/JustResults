using Hechtag.JustResults;
using Hechtag.JustResults.Extensions;
using Hechtag.JustResults.Option;
using System.Diagnostics;

var qwe = Task.Run(async () =>
{
    await Task.Delay(10_000);
    return "spät".ToSuccess();
});

//var opt = Option<string>.Success("Optional!");
var opt = Option<string>.Failure();

var asd = await (
    from a in "hallo".ToSuccess().ToTask()
    from b in "hallo".ToSuccess()
    from c in "hallo".ToTask().ToSuccess()
    from d in opt.ToResult("There was nothing inside the option")
    from e in qwe
    select $"{a} + {b} + {c} + {d} # {e}");

//asd.Tap(Console.WriteLine, e => Console.WriteLine(e.Display));
Console.WriteLine(asd);
Console.ReadKey();

CancellationTokenSource cts = new CancellationTokenSource();
var watch = Stopwatch.StartNew();
var bla = Create().MapAsync(async t => { await Task.Delay(2000, cts.Token); return t; });
await Task.Delay(1000);
cts.Cancel();
Console.WriteLine(await bla);
watch.Stop();
Console.WriteLine(watch.Elapsed.ToString());
Console.ReadKey();

Task<Result<int>> Create()
{
    //await Task.Delay(3000, ct);
    //ct.ThrowIfCancellationRequested();
    var asd = "ergebnis".ToSuccess().ToTask()
        .MapAsync(d => d + "   1")
        .BindAsync(d => d.Length % 2 == 0 
        ? d.Length.ToSuccess().ToTask() 
        : Result<int>.Failure("ungerade").ToTask());

    return asd;
}