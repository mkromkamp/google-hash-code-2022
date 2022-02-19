using HashCode;
using HashCode.Parsers;

Console.WriteLine("Hello, World!");

foreach (var input in Directory.EnumerateFiles("_input/"))
{
    Console.WriteLine($"Solving {input}");
    
    var challenge = new Input().Parse(input);
    var solution = Solutution.SolveChallenge(challenge);
    
    Console.WriteLine($"Estimated score {solution.Score()}");
    new Output().Write(solution);
}

Console.WriteLine("All done :)");