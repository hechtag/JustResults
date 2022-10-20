// See https://aka.ms/new-console-template for more information


using Results;

var first = Stuff.Create("testname");
var second = Stuff.Create("  ");
Console.WriteLine(first);
Console.WriteLine(second);

var list = new List<string>
{
    "asd",
    "asd",
    "fdgklhsdf",
};

list.TraverseApply(Stuff.Create).Tap(
    l => l.ToList().ForEach(Console.WriteLine),
    Console.WriteLine);

public record Stuff
{
    private Stuff(string name) => Name = name;

    public static Result<Stuff> Create(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Result<Stuff>.Failure($"{nameof(Name)} cannot be null or white space. ")
            : Result<Stuff>.Success(new Stuff(input));
    }

    public string Name { get; }
}