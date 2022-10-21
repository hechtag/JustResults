using Results;


var asd =
    from a in Result<string>.Success("hallo")
    from b in Result<string>.Failure("hallo")
    from c in Result<string>.Success("hallo")
    select $"{a} + {b} + {c}";

asd.Tap(Console.WriteLine, e => Console.WriteLine(e.Display));
