using Hechtag.JustResults;

var qwe = Task.Run(async () =>
{
    await Task.Delay(10_000);
    return Result<string>.Success("spät");
});
var asd = await (
    from a in Result<string>.Success("hallo").ToTask()
    from b in Result<string>.Success("hallo")
    from c in Result<string>.Success("hallo").ToTask()
    from d in qwe
    select $"{a} + {b} + {c} # {d}");

//asd.Tap(Console.WriteLine, e => Console.WriteLine(e.Display));
Console.WriteLine(asd);
Console.ReadKey();