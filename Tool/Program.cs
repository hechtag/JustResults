// See https://aka.ms/new-console-template for more information





Console.WriteLine("start");
var result = await Method(Task.Run(async () =>
{
    await Task.Delay(5000);
    return "test-string";
}));
Console.WriteLine(result);


async Awaitable<string> Method(Task<string> input)
{
    var str = await new Awaitable<string>(input);
    return str + " asd";
}
//
// var asd = AsyncResult<string>.Success(Task.Run(() =>
// {
//     Console.WriteLine("del");
//     Task.Delay(2000);
//     Console.WriteLine("del");
//     return "Test";
// }));
//
//
//
// Console.WriteLine(asd);
// Console.WriteLine(asd.IsSuccess);
// Console.WriteLine(asd.GetError());
// Console.WriteLine(await asd.GetValue());
//
//
// ;
// Console.WriteLine(await (await asd.Map(t => t + " asdasd")).GetValue());

//
// var first = Stuff.Create("testname");
// var second = Stuff.Create("  ");
// Console.WriteLine(first);
// Console.WriteLine(second);
//
// var list = new List<string>
// {
//     "asd",
//     "asd",
//     "fdgklhsdf",
// };
//
// list.TraverseApply(Stuff.Create).Tap(
//     l => l.ToList().ForEach(Console.WriteLine),
//     Console.WriteLine);
//
// public record Stuff
// {
//     private Stuff(string name) => Name = name;
//
//     public static Result<Stuff> Create(string input)
//     {
//         return string.IsNullOrWhiteSpace(input)
//             ? Result<Stuff>.Failure($"{nameof(Name)} cannot be null or white space. ")
//             : Result<Stuff>.Success(new Stuff(input));
//     }
//
//     public string Name { get; }
// }